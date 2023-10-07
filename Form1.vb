Imports System.Runtime.CompilerServices

Public Class MainForm
    Private Id As Integer = 0
    Private Firstname As String = ""
    Private Lastname As String = ""
    Private Grade As Integer = 0
    Private intRow As Integer = 0

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: esta línea de código carga datos en la tabla 'Db_confDataSet.usuario' Puede moverla o quitarla según sea necesario.
        Me.UsuarioTableAdapter.Fill(Me.Db_confDataSet.usuario)
        'TODO: esta línea de código carga datos en la tabla 'Db_confDataSet.usuario' Puede moverla o quitarla según sea necesario.
        Me.UsuarioTableAdapter.Fill(Me.Db_confDataSet.usuario)
        'TODO: esta línea de código carga datos en la tabla 'Db_confDataSet.usuario' Puede moverla o quitarla según sea necesario.
        Me.UsuarioTableAdapter.Fill(Me.Db_confDataSet.usuario)
        'TODO: esta línea de código carga datos en la tabla 'Db_confDataSet.usuario' Puede moverla o quitarla según sea necesario.
        Me.UsuarioTableAdapter.Fill(Me.Db_confDataSet.usuario)
        ResetFields()
        FetchData()
    End Sub

    Private Sub ResetFields()
        Me.Id = 0
        txtName.Text = ""
        txtLast.Text = ""
        txtGrade.Text = ""
    End Sub

    Private Sub FetchData()
        SQL = "SELECT * FROM usuario ORDER BY id asc"
        Cmd = New Npgsql.NpgsqlCommand(SQL, Conn)
        Cmd.Parameters.Clear()

        Dim dataTable As DataTable = execQuery(Cmd)

        If dataTable.Rows.Count > 0 Then
            intRow = Convert.ToInt32(dataTable.Rows.Count.ToString())
        Else
            intRow = 0
        End If

        ToolStripStatusLabel1.Text = intRow.ToString() & "Filas recuperadas"

        With DataGridView1
            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AutoGenerateColumns = True
            .DataSource = dataTable

            .Columns(0).HeaderText = "ID"
            .Columns(1).HeaderText = "NOMBRE"
            .Columns(2).HeaderText = "APELLIDO"
            .Columns(3).HeaderText = "NOTA"

            .Columns(0).Width = 50
            .Columns(1).Width = 100
            .Columns(2).Width = 100
            .Columns(3).Width = 50
        End With
    End Sub

    Private Sub Excecute(SQL As String, Optional Action As String = "")
        Cmd = New Npgsql.NpgsqlCommand(SQL, Conn)
        AddParameters(Action)
        execQuery(Cmd)
    End Sub

    Private Sub AddParameters(action As String)
        Cmd.Parameters.Clear()
        Cmd.Parameters.AddWithValue("Firstname", txtName.Text.Trim())
        Cmd.Parameters.AddWithValue("Lastname", txtLast.Text.Trim())
        Cmd.Parameters.AddWithValue("Grade", Integer.Parse(txtGrade.Text))

        If action = "U" Or action = "D" And Me.Id <> 0 Then
            Cmd.Parameters.AddWithValue("Id", Me.Id)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrEmpty(txtName.Text.Trim()) Or String.IsNullOrEmpty(txtLast.Text.Trim()) Then
            MessageBox.Show("Nombre, apellido y nota es obligatorio", "Completar información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim action As String = "C"

        If Me.Id <> 0 Then
            SQL = "UPDATE usuario SET nombre = @Firstname, apellido = @Lastname, nota = @Grade WHERE id = @Id::integer"
            action = "U"
        Else
            SQL = "INSERT INTO usuario (nombre, apellido, nota) VALUES (@Firstname, @Lastname, @Grade)"
        End If

        Excecute(SQL, action)

        MessageBox.Show("Información guardada con éxito", "Datos insertados", MessageBoxButtons.OK, MessageBoxIcon.Information)

        FetchData()
        ResetFields()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If Me.Id = 0 Then
            MessageBox.Show("Seleccione un registro para eliminar", "Imposible de eliminar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        SQL = "DELETE FROM usuario WHERE id = @Id::integer"
        Excecute(SQL, "D")

        FetchData()
        ResetFields()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            Dim dataGridV As DataGridView = DataGridView1
            If e.RowIndex <> -1 Then
                Me.Id = Convert.ToString(dataGridV.CurrentRow.Cells(0).Value).Trim()
                btnSave.Text = "Actualizar"

                txtName.Text = Convert.ToString(dataGridV.CurrentRow.Cells(1).Value).Trim()
                txtLast.Text = Convert.ToString(dataGridV.CurrentRow.Cells(2).Value).Trim()
                txtGrade.Text = Convert.ToString(dataGridV.CurrentRow.Cells(3).Value).Trim()
            End If
        Catch ex As Exception
            Console.WriteLine("[Form1.vb][DataGridView1_CellClick]:" & ex.Message)
        End Try
    End Sub
End Class
