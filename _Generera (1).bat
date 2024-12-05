:: Name of the zip file to generate
set "zipFile=_LADDA_UPP_MIG.zip"

:: The name and path of the current batch-file
set "batchFile=%~n0%~x0"

:: Create a zip that excludes the zip file, the batch file and the .vs/bin/obj folders
tar -acf %zipFile% --exclude=%zipFile% --exclude=%batchFile% --exclude=".vs" --exclude="bin" --exclude="obj" *
