forfiles /p "C:\南京信息中心接口服务\LogDF"  /d -7 /c "cmd /c if @ISDIR==TRUE rd /s/q @path"

