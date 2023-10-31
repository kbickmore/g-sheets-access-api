# Google Drive Project
this is just a simple project being used to test reading from the Google Drive specifically for g sheets

## Setup
### Google Dev Console
First, you need to setup a project within the cloud to use Google's apis.

- go to [google cloud console](https://console.cloud.google.com/)
- create a new project, ther should be a `New Project` button in the center of the page
- fill out the details for the project
### Enable Google Sheet Service
We will then need to enable the Google Sheets API for this project
- First, go to your project welcome page `https://console.cloud.google.com/welcome?project=your_project_name`
- click the navigation hamburger in the top left, and click on `API & Services -> enabled API & Services`
- there should be a button to enable apis & services, click on that
- Search for `Google Sheets API`
- enable the service
### Service Account
in order to setup a service account, you need to have folowed the steps above to create a cloud console project.
- First, go to your project welcome page `https://console.cloud.google.com/welcome?project=your_project_name`
- click the navigation hamburger in the top left, and click on `IAM & Admin -> Service Accounts`
- Click `Create Service Account`
- Give it a name, give it a grant type of the lowest amount that you can, and add whatever users need to manage the service account
### Service Account Key
From here we will need to generate a key for the service account.
- Click on the email for the service account to go to the Service account details tab
- click on the `Keys` tab
- Add a key, and copy its value, this will be your `GoogleSheetsAPIKey` for accessing the api with the service account email.

### Secrets
Right now I have a lot of items in secrets, mainly just to hide the files I'm testing in my own google drive account.
- GoogleSheetsAPIKey - this is an API key this is generated by folowing the section above within a service account
- GoogleSheetsAPIServiceAccountEmail - this is the email associated with a service account
- GoogleSheetsAPIFileId - this can be found in the url of the file you want to allow the service account to manipulate.example: `https://docs.google.com/spreadsheets/d/your_spreadsheet_id/edit#gid=some_other_id`
- GoogleSheetsAPISheetName - This is not really a secret, but it is the sheet name within the file.
