using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BusinessLogic.Helper
{

    public static class KeyCodeDictionary
    {
        public const string LoaiVanBan = "KeyVanBan";
        public const string CoQuanBanHanh = "KeyCoQuanBanHanh";
        public const string DonVi = "KeyDonVi";
        public const string ChuyenMucTinTuc = "KeyChuyenMucTinTuc";
        public const string LienKetWebsite = "KeyConfigWebsite";
        public const string LinkThuDienThu = "KeyLinkThuDienTu";
        public const string StudyProgram = "KeyChuongTrinhHoc";
    }

    public static class CommonHelper
    {
        public const string PageUrl = "hoat-dong";
        public enum TypePage
        {
            News = 1,
            Silder 
        }
    }
}
