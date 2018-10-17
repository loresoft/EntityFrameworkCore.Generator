#if !SILVERLIGHT
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace EntityFrameworkCore.Generator.Reflection
{
    internal static class DynamicMethodFactory
    {
        public static Func<object, object[], object> CreateMethod(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException(nameof(methodInfo));

            var dynamicMethod = CreateDynamicMethod(
                "Dynamic" + methodInfo.Name,
                typeof(object),
                new[] { typeof(object), typeof(object[]) },
                methodInfo.DeclaringType);

            var generator = dynamicMethod.GetILGenerator();
            var parameters = methodInfo.GetParameters();

            var paramTypes = new Type[parameters.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                var parameterInfo = parameters[i];
                if (parameterInfo.ParameterType.IsByRef)
                    paramTypes[i] = parameterInfo.ParameterType.GetElementType();
                else
                    paramTypes[i] = parameterInfo.ParameterType;
            }

            var locals = new LocalBuilder[paramTypes.Length];
            for (int i = 0; i < paramTypes.Length; i++)
                locals[i] = generator.DeclareLocal(paramTypes[i], true);

            for (int i = 0; i < paramTypes.Length; i++)
            {
                generator.Emit(OpCodes.Ldarg_1);
                generator.FastInt(i);
                generator.Emit(OpCodes.Ldelem_Ref);
                generator.UnboxIfNeeded(paramTypes[i]);
                generator.Emit(OpCodes.Stloc, locals[i]);
            }

            if (!methodInfo.IsStatic)
                generator.Emit(OpCodes.Ldarg_0);

            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (parameters[i].ParameterType.IsByRef)
                    generator.Emit(OpCodes.Ldloca_S, locals[i]);
                else
                    generator.Emit(OpCodes.Ldloc, locals[i]);
            }

            if (methodInfo.IsStatic)
                generator.EmitCall(OpCodes.Call, methodInfo, null);
            else
                generator.EmitCall(OpCodes.Callvirt, methodInfo, null);

            if (methodInfo.ReturnType == typeof(void))
                generator.Emit(OpCodes.Ldnull);
            else
                generator.BoxIfNeeded(methodInfo.ReturnType);

            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (!parameters[i].ParameterType.IsByRef)
                    continue;

                generator.Emit(OpCodes.Ldarg_1);
                generator.FastInt(i);
                generator.Emit(OpCodes.Ldloc, locals[i]);

                var localType = locals[i].LocalType;
                if (localType.GetTypeInfo().IsValueType)
                    generator.Emit(OpCodes.Box, localType);

                generator.Emit(OpCodes.Stelem_Ref);
            }

            generator.Emit(OpCodes.Ret);

            return dynamicMethod.CreateDelegate(typeof(Func<object, object[], object>)) as Func<object, object[], object>;
        }

        public static Func<object> CreateConstructor(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var dynamicMethod = CreateDynamicMethod(
                "Create" + type.FullName,
                typeof(object),
                Type.EmptyTypes,
                type);

            dynamicMethod.InitLocals = true;
            var generator = dynamicMethod.GetILGenerator();

            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsValueType)
            {
                generator.DeclareLocal(type);
                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Box, type);
            }
            else
            {
                var constructorInfo = typeInfo.GetConstructor(Type.EmptyTypes);
                if (constructorInfo == null)
                    throw new InvalidOperationException($"Could not get constructor for {type}.");

                generator.Emit(OpCodes.Newobj, constructorInfo);
            }

            generator.Return();

            return dynamicMethod.CreateDelegate(typeof(Func<object>)) as Func<object>;
        }

        public static Func<object, object> CreateGet(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException(nameof(propertyInfo));

            if (!propertyInfo.CanRead)
                return null;

            var methodInfo = propertyInfo.GetGetMethod(true);
            if (methodInfo == null)
                return null;

            var dynamicMethod = CreateDynamicMethod(
                "Get" + propertyInfo.Name,
                typeof(object),
                new[] { typeof(object) },
                propertyInfo.DeclaringType);

            var generator = dynamicMethod.GetILGenerator();

            if (!methodInfo.IsStatic)
                generator.PushInstance(propertyInfo.DeclaringType);

            generator.CallMethod(methodInfo);
            generator.BoxIfNeeded(propertyInfo.PropertyType);
            generator.Return();

            return dynamicMethod.CreateDelegate(typeof(Func<object, object>)) as Func<object, object>;
        }

        public static Func<object, object> CreateGet(FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
                throw new ArgumentNullException(nameof(fieldInfo));

            var dynamicMethod = CreateDynamicMethod(
                "Get" + fieldInfo.Name,
                typeof(object),
                new[] { typeof(object) },
                fieldInfo.DeclaringType);

            var generator = dynamicMethod.GetILGenerator();

            if (fieldInfo.IsStatic)
                generator.Emit(OpCodes.Ldsfld, fieldInfo);
            else
                generator.PushInstance(fieldInfo.DeclaringType);

            generator.Emit(OpCodes.Ldfld, fieldInfo);
            generator.BoxIfNeeded(fieldInfo.FieldType);
            generator.Return();

            return dynamicMethod.CreateDelegate(typeof(Func<object, object>)) as Func<object, object>;
        }

        public static Action<object, object> CreateSet(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException(nameof(propertyInfo));
            if (!propertyInfo.CanWrite)
                return null;

            var methodInfo = propertyInfo.GetSetMethod(true);
            if (methodInfo == null)
                return null;

            var dynamicMethod = CreateDynamicMethod(
                "Set" + propertyInfo.Name,
                null,
                new[] { typeof(object), typeof(object) },
                propertyInfo.DeclaringType);

            var generator = dynamicMethod.GetILGenerator();

            if (!methodInfo.IsStatic)
                generator.PushInstance(propertyInfo.DeclaringType);

            generator.Emit(OpCodes.Ldarg_1);
            generator.UnboxIfNeeded(propertyInfo.PropertyType);
            generator.CallMethod(methodInfo);
            generator.Return();

            return dynamicMethod.CreateDelegate(typeof(Action<object, object>)) as Action<object, object>;
        }

        public static Action<object, object> CreateSet(FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
                throw new ArgumentNullException(nameof(fieldInfo));

            var dynamicMethod = CreateDynamicMethod(
                "Set" + fieldInfo.Name,
                null,
                new[] { typeof(object), typeof(object) },
                fieldInfo.DeclaringType);

            var generator = dynamicMethod.GetILGenerator();

            if (fieldInfo.IsStatic)
                generator.Emit(OpCodes.Ldsfld, fieldInfo);
            else
                generator.PushInstance(fieldInfo.DeclaringType);

            generator.Emit(OpCodes.Ldarg_1);
            generator.UnboxIfNeeded(fieldInfo.FieldType);
            generator.Emit(OpCodes.Stfld, fieldInfo);
            generator.Return();

            return dynamicMethod.CreateDelegate(typeof(Action<object, object>)) as Action<object, object>;
        }


        private static DynamicMethod CreateDynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner)
        {
            var typeInfo = owner.GetTypeInfo();
            return !typeInfo.IsInterface
                ? new DynamicMethod(name, returnType, parameterTypes, owner, true)
                : new DynamicMethod(name, returnType, parameterTypes, typeInfo.Assembly.ManifestModule, true);
        }
    }
}
#endif