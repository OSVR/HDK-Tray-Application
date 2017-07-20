# OSVR-TrayApp
Application that runs in the Windows Notification Tray.

## Setup ##
If the repository does not include a certificate file (`.pxf`), you'll get the following build error: `Unable to find manifest signing certificate in the certificate store.`

To fix that, you'll need to generate a temporary signing certificate in order to build locally.  To do that:

1. Right click on the project and select `Properties`
1. Navigate to the `Signing` tab
1. Select `Create Test Certificate`
1. Enter a password, e.g. "test" (SHA256 is fine), and click `OK`
1. Save project settings (`Ctrl+S`)

If you encounter an error similar to this: `"An error occurred while signing`...`SignTool.exe was not found at path.` then you performed a selective installation of Visual Studio and it didn't include the signing tool.  You'll need to install `ClickOncePublishing Tools`.  On Windows 10:

1. Close Visual Studio
1. Open `Programs and Features`
1. Select `Microsoft Visual Studio 2015` and click `Change`
1. Click `Modify`
1. Expand `Windows and Web Development`, then tick `ClickOnce Publishing Tools` for installation
1. Click `Update`
1. Wait an unreasonably long time
1. Start Visual studio
