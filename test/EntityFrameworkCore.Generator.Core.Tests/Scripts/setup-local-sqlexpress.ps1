$conn = [System.Data.SqlClient.SqlConnection]::new("Data Source=(local)\SQLEXPRESS;Integrated Security=True")
$conn.Open()

$cmnd = $conn.CreateCommand(
$cmnd.CommandText = "CREATE DATABASE EFG_TrackerTest"
$cmnd.ExecuteNonQuery()
$cmnd.Dispose()

$conn.Close()
$conn.Dispose()
