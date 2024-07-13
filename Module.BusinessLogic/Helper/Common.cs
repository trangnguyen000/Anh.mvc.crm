using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BusinessLogic.Helper
{
    public static class Common
    {
        public enum TyoeFromDataSource
        {
            Website = 0,
            Facebook = 0,
        }

        public enum StatusContactSupport
        {
            New = 1,
            Inprogress = 2,
            Complete = 3,
            Cancel = 4
        }

        public static Dictionary<short, string> StatusContactSupports = new Dictionary<short, string>
        {
             { (short)StatusContactSupport.New, "Mới" },
             { (short)StatusContactSupport.Inprogress, "Đang hỗ trợ" },
               { (short)StatusContactSupport.Complete, "Hoàn thành" },
                 { (short)StatusContactSupport.Cancel, "Hủy" },
        };


    }
}
