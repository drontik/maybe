'=========================[ Start of file ]========================= 
'  File:         ReplaceInFiles.vbs 
'  Description:  Производит поиск текста с заменой 
'                в выделенных текстовых файлах      
'  Installation: Создайте кнопку на панели TC 
'                Команда: "диск:\путь\к\файлу\ReplaceInFiles.vbs" 
'                Параметры: "%L" 
'  Copyright:    (c) 2006-2008, Volniy 

Option Explicit 

Dim FSO, Find_String, Replace_String, F, Buffer, ListFile, Found 
Const ForReading = 1, ForWriting = 2 

Set FSO = CreateObject("Scripting.FileSystemObject") 

Find_String = InputBox("Введите искомую строку", "Поиск и замена") 
If Len(Find_String) = 0 Then 
	MsgBox "Не задана строка поиска!", vbExclamation, "Поиск и замена" 
	WScript.Quit() 
End If 

Replace_String = InputBox("Введите строку для замены", "Поиск и замена") 

Set ListFile = FSO.OpenTextFile(WScript.Arguments(0), 1) 
Do While Not ListFile.AtEndOfStream 
	Call DoItForThisFile(ListFile.ReadLine) 
Loop 
ListFile.Close 
Set ListFile = Nothing 
Set F = Nothing 
Set FSO = Nothing 

MsgBox "Поиск с заменой завершен!", vbInformation, "Поиск и замена" 
WScript.Quit 

Sub DoItForThisFile(FilePath) 
	Set F = FSO.OpenTextFile(FilePath, ForReading) 
	Buffer = F.ReadAll 
	F.Close 
	Found = InStr(1, Buffer, Find_String, 1) 
	If Found <> 0 Then 
		Buffer = Replace(CStr(Buffer), Find_String, Replace_String,1,-1,1) 
		'FSO.CopyFile FilePath, FilePath & ".bak" 
		Set F = FSO.OpenTextFile(FilePath, ForWriting) 
		F.Write Buffer 
		F.Close 
	End If 
End Sub 
'=========================[  End of file  ]=========================