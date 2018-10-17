using System;
using FluentAssertions;
using Xunit;

namespace EntityFrameworkCore.Generator.Core.Tests
{
    public class NameFormatterTest
    {
        [Fact]
        public void Example()
        {
            var o = new { First = "John", Last = "Doe" };

            string result = NameFormatter.Format("Full Name: {First} {Last}", o);
            result.Should().Be("Full Name: John Doe");
        }

        [Fact]
        public void StringFormat_WithMultipleExpressions_NullProperty()
        {
            var o = new { foo = 123.45, bar = 42, baz = (string)null };

            string result = NameFormatter.Format("{foo} {foo} {bar}{baz}", o);
            result.Should().Be("123.45 123.45 42");
        }

        [Fact]
        public void StringFormat_WithMultipleExpressions_FormatsThemAll()
        {
            var o = new { foo = 123.45, bar = 42, baz = "hello" };

            string result = NameFormatter.Format("{foo} {foo} {bar}{baz}", o);
            result.Should().Be("123.45 123.45 42hello");
        }

        [Fact]
        public void StringFormat_WithDoubleEscapedCurlyBraces_DoesNotFormatString()
        {
            var o = new { foo = 123.45 };

            string result = NameFormatter.Format("{{{{foo}}}}", o);
            result.Should().Be("{{foo}}");
        }

        [Fact]
        public void StringFormat_WithFormatSurroundedByDoubleEscapedBraces_FormatsString()
        {
            var o = new { foo = 123.45 };

            string result = NameFormatter.Format("{{{{{foo}}}}}", o);
            result.Should().Be("{{123.45}}");
        }

        [Fact]
        public void Format_WithEscapeSequence_EscapesInnerCurlyBraces()
        {
            var o = new { foo = 123.45 };

            string result = NameFormatter.Format("{{{foo}}}", o);
            result.Should().Be("{123.45}");
        }

        [Fact]
        public void Format_WithEmptyString_ReturnsEmptyString()
        {
            var o = new { foo = 123.45 };

            string result = NameFormatter.Format(string.Empty, o);
            result.Should().Be(string.Empty);
        }

        [Fact]
        public void Format_WithNoFormats_ReturnsFormatStringAsIs()
        {
            var o = new { foo = 123.45 };

            string result = NameFormatter.Format("a b c", o);
            result.Should().Be("a b c");
        }

        [Fact]
        public void Format_WithFormatType_ReturnsFormattedExpression()
        {
            var o = new { foo = 123.45 };

            string result = NameFormatter.Format("{foo:#.#}", o);
            result.Should().Be("123.5");
        }

        [Fact]
        public void Format_WithSubProperty_ReturnsValueOfSubProperty()
        {
            var o = new { foo = new { bar = 123.45 } };

            string result = NameFormatter.Format("{foo.bar:#.#}ms", o);
            result.Should().Be("123.5ms");
        }

        [Fact]
        public void Format_WithFormatNameNotInObject_ThrowsFormatException()
        {
            var o = new { foo = 123.45 };

            Action f = () => NameFormatter.Format("{bar}", o);
            f.Should().Throw<FormatException>();

        }

        [Fact]
        public void Format_WithNoEndFormatBrace_ThrowsFormatException()
        {
            var o = new { foo = 123.45 };

            Action f = () => NameFormatter.Format("{bar", o);
            f.Should().Throw<FormatException>();
        }

        [Fact]
        public void Format_WithEscapedEndFormatBrace_ThrowsFormatException()
        {
            var o = new { foo = 123.45 };

            Action f = () => NameFormatter.Format("{foo}}", o);
            f.Should().Throw<FormatException>();
        }

        [Fact]
        public void Format_WithDoubleEscapedEndFormatBrace_ThrowsFormatException()
        {
            var o = new { foo = 123.45 };

            Action f = () => NameFormatter.Format("{foo}}}}bar", o);
            f.Should().Throw<FormatException>();
        }

        [Fact]
        public void Format_WithDoubleEscapedEndFormatBraceWhichTerminatesString_ThrowsFormatException()
        {
            var o = new { foo = 123.45 };

            Action f = () => NameFormatter.Format("{foo}}}}", o);
            f.Should().Throw<FormatException>();
        }

        [Fact]
        public void Format_WithEndBraceFollowedByEscapedEndFormatBraceWhichTerminatesString_FormatsCorrectly()
        {
            var o = new { foo = 123.45 };

            string result = NameFormatter.Format("{foo}}}", o);
            result.Should().Be("123.45}");
        }

        [Fact]
        public void Format_WithEndBraceFollowedByEscapedEndFormatBrace_FormatsCorrectly()
        {
            var o = new { foo = 123.45 };

            string result = NameFormatter.Format("{foo}}}bar", o);
            result.Should().Be("123.45}bar");
        }

        [Fact]
        public void Format_WithEndBraceFollowedByDoubleEscapedEndFormatBrace_FormatsCorrectly()
        {
            var o = new { foo = 123.45 };

            string result = NameFormatter.Format("{foo}}}}}bar", o);
            result.Should().Be("123.45}}bar");
        }

        [Fact]
        public void Format_WithNullFormatString_ThrowsArgumentNullException()
        {
            Action f = () => NameFormatter.Format(null, 123);
            f.Should().Throw<ArgumentNullException>();
        }



    }
}
