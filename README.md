# ZipFolderPSScriptCreator
Create a powershell backup script or single folder zip backup with windows form
Created in Visual Studio Community and written in C# and powershell on Windows 10
Is a windows form that will let you select a single folder to backup. Limited by using the folderDialog to one selection.
You can either perform a one time zip using ZipFile or select a folder to hold your backup zip file when the script in powershell is 
created.
The powershell script will check if the file exists and delete it if it does and then zip the folder to its backup location.
Not the most usefull form but better than manually selecting your backup every few days or week. Just run the script and save a few clicks

Go to \bin\release\zipattempt application to copy to a location and run the windows form
