Imports Npgsql

Module Postgres
    Public Conn As New NpgsqlConnection(GetConnection())
    Public Cmd As NpgsqlCommand
    Public SQL As String = ""

    Public Function GetConnection() As String
        Dim host As String = "Host=db-conferencia.coa49vnnhx5t.us-east-1.rds.amazonaws.com;"
        Dim port As String = "Port=5432;"
        Dim db As String = "Database=db_conf;"
        Dim user As String = "Username=macochave;"
        Dim pass As String = "Password=Soy.tu.amo;"
        Dim ssl As String = "SSL Mode=Require;"
        Dim requireServer = "Trust Server Certificate=True;"

        Dim strConn As String = String.Format("{0}{1}{2}{3}{4}{5}{6}", host, port, db, user, pass, ssl, requireServer)

        ''' Return "Host=db-conferencia.coa49vnnhx5t.us-east-1.rds.amazonaws.com;Database=db_conf;Username=macochave;Password=Soy.tu.amo;SSL Mode=Require;TrustServerCertificate=True"

        Return strConn
    End Function

    Public Function execQuery(Cmd As NpgsqlCommand) As DataTable
        Dim adapter As NpgsqlDataAdapter
        Dim dataTable As New DataTable()

        Try
            adapter = New NpgsqlDataAdapter
            adapter.SelectCommand = Cmd
            adapter.Fill(dataTable)

            Return dataTable
        Catch ex As Exception
            Console.WriteLine("[Postgres.vb][execQuery]:" & ex.Message)
            MessageBox.Show("Ocurrió un error " & ex.Message, "Error en realizar una consulta", MessageBoxButtons.OK, MessageBoxIcon.Error)
            dataTable = Nothing
        End Try

        Return dataTable
    End Function

End Module
