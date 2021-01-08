# csharpi
Repository for seanbot, the discord hassler


Pi depoyment instructions:
cd /home/pi/csharpi
git pull

dotnet publish -o /home/pi/bot
cd /home/pi/bot
dotnet csharpi.dll


Only make changes on the `Command.cs` file
