Intent is to be able to call it like Git commands.
Typical patterns  
  tool.exe noun verb options
  tool.exe verb noun options
  
Sample should show implementing commands that run in these forms:

util.exe screen add -name "myScreen"         
util.exe screen list
util.exe screen remove "myScreen"
util.exe logs list

--or--
 
deployer.exe provision database --name "dbname"
deployer.exe teardown environment 
