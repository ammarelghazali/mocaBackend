using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Settings
{
    public class FileSettings
    {
        public string Location_FilePath { get; set; }
        public string WorkSpace_FilePath { get; set; }
        public string WorkSpace_ImagesPath { get; set; }
        public string EventSpace_FilePath { get; set; }
        public string EventSpace_ImagesPath { get; set; }
        public string MeetingRoom_FilePath { get; set; }
        public string MeetingRoom_ImagesPath { get; set; }
        public string Users_ImagesPath { get; set; }
        public string Splash_Video_Path { set; get; }
        public string Client_ProfilePicPath { get; set; }
        public string Restaurant_Main_Category_ImagePath { get; set; }
        public string Restaurant_Menu_Items_ImagePath { get; set; }
        public string Restaurant_o_ImagePath { get; set; }
        public string Template_Email_ImagePath { get; set; }
        public string AssetsPropertyTypesImagesUrl { get; set; }
        public string UploadedDocuments { get; set; }
        public string Server_Base_Url { get; set; }
        public string Personal_Profile_Base_Url { get; set; }
        public string HR_Employees_ImagePath { get; set; }
        public string Employees_Deliverables_ImagePath { get; set; }
        public string Foodics_Base_Url { get; set; }
        public string Foodics_Token { get; set; }
        public string Winfi_Base_Url { get; set; }
        public string WorkSpace_Furnitures_ImagePath { get; set; }
        public int MaxSizeInMega { get; set; }
        public string Inclusion_IconPath { get; set; }
        public string Occupancy_IconPath { get; set; }
        public string Email_Path { get; set; }
        public string SenderName { get; set; }
        public string PaytabLink { get; set; }
        public string PaytabKey { get; set; }
        public string WebhookCallback { get; set; }
        public string WebhookReturn { get; set; }
        public string PassUrl { get; set; }
    }
}
