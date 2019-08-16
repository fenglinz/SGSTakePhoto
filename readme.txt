PhotoApp.exe -c f:/1.png   拍照功能
   返回json格式 如失败：{"result":"error","reason":"dir is not exist"}
		成功：{"Paths":["g:/temp/20190816_10-33-18-110.jpg","g:/temp/20190816_10-33-18-566.jpg"],"result":"success"}

PhotoApp.exe -e f:/1.png   编辑功能
   返回json格式 如失败：{"result":"error","reason":"file is not exist"}
		成功：{"Paths":"g:/temp/1_edit.png","result":"success"}

