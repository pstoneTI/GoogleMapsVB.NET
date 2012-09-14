﻿Public Class GooglePlacesS
    Dim buscar As String
    Dim latlongAux(100, 100)
    Private Sub GooglePlacesS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label16.Visible = False
        Label19.Visible = False
        Label20.Visible = False
        Label21.Visible = False
        Label22.Visible = False
        For Each c As Object In Me.Controls
            If c.GetType Is GetType(TextBox) Then
                AddHandler DirectCast(c, TextBox).GotFocus, AddressOf conFoco
                AddHandler DirectCast(c, TextBox).LostFocus, AddressOf sinFoco
            End If
        Next

        For Each c As Object In Me.Controls
            If c.GetType Is GetType(TextBox) Then
                AddHandler DirectCast(c, TextBox).KeyDown, AddressOf BuscarD
            End If
        Next
        For Each c As Object In Me.Controls
            If c.GetType Is GetType(Label) Then
                AddHandler DirectCast(c, Label).MouseUp, AddressOf BuscarD
            End If
        Next
        Timer1.Enabled = False
        ocultar()
    End Sub
    Private Sub conFoco(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, TextBox).BackColor = Color.LightCyan
    End Sub

    Private Sub sinFoco(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, TextBox).BackColor = Color.White
    End Sub

    Private Sub BuscarD(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim contador = 0
        For Each c As Control In Me.Controls
            If TypeOf c Is Label Then
                If c.ForeColor = Color.White Then
                    contador += 1
                End If
            End If
        Next
        If contador >= 2 And txtpoblacion.Text <> "" And txtcalle.Text <> "" And txtprovincia.Text <> "" Then

            For Each c As Control In Me.Controls
                If TypeOf c Is Label Then
                    If c.ForeColor = Color.White Then
                        Select Case c.Text
                            Case "Cajero automático"
                                buscar = "atm"
                            Case "Gasolinera"
                                buscar = "gas_station"
                            Case "Hospital"
                                buscar = "hospital"
                            Case "Bar"
                                buscar = "bar"
                            Case "Cine"
                                buscar = "movie_theater"
                            Case "Restaurante"
                                buscar = "restaurant"
                            Case "Estación de autobuses"
                                buscar = "bus_station"
                            Case "Estación de tren"
                                buscar = "train_station"
                            Case "Museo"
                                buscar = "museum"
                        End Select
                        Timer1.Enabled = True
                    End If
                End If
            Next
        End If
    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim onobjetoMaps As New GoogleMaps
        Dim objetoMpas As New GoogleMaps
        Dim datos(1000) As String
        datos(0) = ""
        Dim latlong(1) As Double
        Dim calle As String
        calle = txtcalle.Text
        If txtcalle.Text <> "" Then
            calle = calle.ToLower()
            Dim posicion = calle.Contains("calle")
            If posicion = False Then
                calle = "calle " + txtcalle.Text
            End If
        Else
            calle = ""
        End If
        latlong = onobjetoMaps.CodificacionGeo(calle, txtpoblacion.Text, txtprovincia.Text)
        If latlong(0) <> 36.778261 And latlong(1) <> -119.4179324 Then


            If IsNumeric(txtradio.Text) Then
                datos = objetoMpas.GooglePlaces(latlong(0), latlong(1), txtradio.Text, buscar)
            Else
                datos = objetoMpas.GooglePlaces(latlong(0), latlong(1), 5000, buscar)
            End If
            Dim datos2(contadorPlaces - 1) As String
            For i = 0 To contadorPlaces - 1
                datos2(i) = datos(i)
            Next
            Label16.Visible = False
            Label19.Visible = False
            Label20.Visible = False
            Label21.Visible = False
            Label22.Visible = False

            PictureBox2.Visible = False
            PictureBox3.Visible = False
            PictureBox4.Visible = False
            PictureBox5.Visible = False
            PictureBox6.Visible = False

            Label16.Text = "Establecimiento: "
            Label19.Text = "Establecimiento: "
            Label20.Text = "Establecimiento: "
            Label21.Text = "Establecimiento: "
            Label22.Text = "Establecimiento: "
            Dim auxiliar(100, 4) As String
            Dim contador As Integer = 0
            Dim URLimagenes(4) As String
            If datos2.Length >= 7 Then
                For i = 0 To UBound(datos2) - 1
                    Select Case datos2(i)
                        Case "name"
                            auxiliar(contador, 0) = datos2(i + 1)

                        Case "lat"
                            auxiliar(contador, 1) = " (" & datos2(i + 1) & ","
                            latlongAux(contador, 0) = datos2(i + 1)

                        Case "lng"
                            auxiliar(contador, 2) = datos2(i + 1) & ")."
                            latlongAux(contador, 1) = datos2(i + 1)

                        Case "rating"
                            If datos2(i + 1) = Nothing Then
                                auxiliar(contador, 3) = "Restaurante no valorado. " & datos2(i + 1)
                            Else
                                auxiliar(contador, 3) = "Valoración: " & datos2(i + 1)
                            End If

                        Case "icon"
                            auxiliar(contador, 4) = datos2(i + 1)
                            If contador <= 4 Then
                                URLimagenes(contador) = datos2(i + 1)
                            End If
                            contador += 1
                    End Select
                Next

            End If

            For i = 0 To 3
                Label16.Text = Label16.Text + auxiliar(0, i)
            Next
            For i = 0 To 3
                Label19.Text = Label19.Text + auxiliar(1, i)
            Next
            For i = 0 To 3
                Label20.Text = Label20.Text + auxiliar(2, i)
            Next
            For i = 0 To 3
                Label21.Text = Label21.Text + auxiliar(3, i)
            Next
            For i = 0 To 3
                Label22.Text = Label22.Text + auxiliar(4, i)
            Next

            PictureBox2.Image = objetoMpas.cargarrecursoweb(URLimagenes(0))
            PictureBox3.Image = objetoMpas.cargarrecursoweb(URLimagenes(1))
            PictureBox4.Image = objetoMpas.cargarrecursoweb(URLimagenes(2))
            PictureBox5.Image = objetoMpas.cargarrecursoweb(URLimagenes(3))
            PictureBox6.Image = objetoMpas.cargarrecursoweb(URLimagenes(4))


            If Label16.Text <> "Establecimiento: " Then Label16.Visible = True : PictureBox2.Visible = True Else Label16.Text = "No se han encontrado resultados :(" : Label16.Visible = True : PictureBox2.Image = objetoMpas.cargarrecursoweb("no resultados") : PictureBox2.Visible = True
            If Label19.Text <> "Establecimiento: " Then Label19.Visible = True : PictureBox3.Visible = True
            If Label20.Text <> "Establecimiento: " Then Label20.Visible = True : PictureBox4.Visible = True
            If Label21.Text <> "Establecimiento: " Then Label21.Visible = True : PictureBox5.Visible = True
            If Label22.Text <> "Establecimiento: " Then Label22.Visible = True : PictureBox6.Visible = True
        Else
            'Evitando resultados irrelevantes
            Label16.Visible = True
            Label19.Visible = False
            Label20.Visible = False
            Label21.Visible = False
            Label22.Visible = False
            Label16.Text = "No se han encontrado resultados :("
            Label16.Visible = True : PictureBox2.Image = objetoMpas.cargarrecursoweb("no resultados")
            PictureBox2.Visible = True
            PictureBox3.Visible = False
            PictureBox4.Visible = False
            PictureBox5.Visible = False
            PictureBox6.Visible = False
        End If




        Timer1.Enabled = False
    End Sub
















    Sub ocultar()
        Label5.Visible = False
        Label6.Visible = False
        Label7.Visible = False
        Label8.Visible = False
        Label9.Visible = False
        Label10.Visible = False
        Label11.Visible = False
        Label12.Visible = False
        Label13.Visible = False
    End Sub
    Sub pintar(ByRef label As Label)
        label.ForeColor = Color.White
    End Sub
    Sub borrar()
        For Each c As Control In Me.Controls
            If TypeOf c Is Label Then
                c.ForeColor = Color.Black
            End If
        Next
    End Sub
    'Sub habilitarTexto()
    '    txtcalle.ReadOnly = False
    '    txtpoblacion.ReadOnly = False
    '    txtprovincia.ReadOnly = False
    '    txtradio.ReadOnly = False
    'End Sub
    'Sub deshabilitarTexto()
    '    txtcalle.ReadOnly = True
    '    txtpoblacion.ReadOnly = True
    '    txtprovincia.ReadOnly = True
    '    txtradio.ReadOnly = True
    'End Sub


    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        ocultar()
        borrar()
        pintar(Label1)
        Label5.Visible = True
        Label6.Visible = True
        Label7.Visible = True
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        ocultar()
        borrar()
        pintar(Label2)
        Label8.Visible = True
        Label9.Visible = True
        Label10.Visible = True
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        ocultar()
        borrar()
        pintar(Label3)
        Label11.Visible = True
        Label12.Visible = True


    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        ocultar()
        borrar()
        pintar(Label4)
        Label13.Visible = True
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        borrar()
        pintar(Label5)
        pintar(Label1)
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        borrar()
        pintar(Label6)
        pintar(Label1)
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        borrar()
        pintar(Label7)
        pintar(Label1)
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        borrar()
        pintar(Label10)
        pintar(Label2)
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        borrar()
        pintar(Label9)
        pintar(Label2)
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        borrar()
        pintar(Label8)
        pintar(Label2)
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        borrar()
        pintar(Label11)
        pintar(Label3)
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        borrar()
        pintar(Label12)
        pintar(Label3)
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        borrar()
        pintar(Label13)
        pintar(Label4)
    End Sub

End Class