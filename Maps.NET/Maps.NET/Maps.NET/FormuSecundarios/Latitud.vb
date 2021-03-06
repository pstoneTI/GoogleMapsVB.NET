﻿Public Class Latitud

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim objetoMaps As New MapsNet
        Dim aspectoFormu As New AspectoFormulario
        If aspectoFormu.verificarnumeros(txtlatitud.Text) And aspectoFormu.verificarnumeros(txtlongitud.Text) Then 'Verificamos que sean numeros
            txtlongitud.ForeColor = Color.Black 'Pintamos de negro el dato correcto
            txtlatitud.ForeColor = Color.Black 'Pintamos de negro el dato correcto
            Dim direccionString = objetoMaps.ObtenerURLdesdelatlong(txtlatitud.Text, txtlongitud.Text) 'String con la direccion
            Dim direccion As New Uri(direccionString) 'Pasamos el string a URI
            If CheckBox1.Checked = True Then 'Decidimos si lo abrimos en pestaña activa o nueva pestaña
                aspectoFormu.NuevaFicha("Latitud/Longitud") 'Abrimos una nueva pestaña
            End If
            'Este código sirve para seleccionar el navegador de la pestaña activa
            Dim navegador = aspectoFormu.NavegadorActual(FormularioPrincipal.TabControl1.SelectedIndex)
            navegador.Url = direccion
            'Añadir al autocompletado
            aspectoFormu.autocompletar({txtlatitud.Text, txtlongitud.Text})

        Else
            If aspectoFormu.verificarnumeros(txtlatitud.Text) = False Then txtlatitud.ForeColor = Color.Red
            If aspectoFormu.verificarnumeros(txtlongitud.Text) = False Then txtlongitud.ForeColor = Color.Red
        End If
    End Sub
     

    Private Sub txtlatitud_GotFocus(sender As Object, e As EventArgs) Handles txtlatitud.GotFocus
        txtlatitud.ForeColor = Color.Black 'Pintamos de negro para actualizar
    End Sub

    Private Sub txtlongitud_GotFocus(sender As Object, e As EventArgs) Handles txtlongitud.GotFocus
        txtlongitud.ForeColor = Color.Black 'Pintamos de negro para actualizar
    End Sub

    Private Sub txtlatitud_KeyDown(sender As Object, e As KeyEventArgs) Handles txtlatitud.KeyDown
        txtlatitud.ForeColor = Color.Black 'Pintamos de negro para actualizar
    End Sub

    Private Sub txtlongitud_KeyDown(sender As Object, e As KeyEventArgs) Handles txtlongitud.KeyDown
        txtlongitud.ForeColor = Color.Black 'Pintamos de negro para actualizar
    End Sub

    Private Sub Latitud_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Indicamos que el txt admite autocompletado
        Dim aspectoFor As New AspectoFormulario
        aspectoFor.CargarControles(Me)
    End Sub
End Class