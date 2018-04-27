using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.Entity
{
    //public class MenuModels
    //{
    //    public string MainMenuName { get; set; }
    //    public int MainMenuId { get; set; }
    //    public string SubMenuName { get; set; }
    //    public int SubMenuId { get; set; }
    //    public string ControllerName { get; set; }
    //    public string ActionName { get; set; }
    //    public int? RoleId { get; set; }
    //    public string RoleName { get; set; }
    //}


    public class MenuModelsList
    {
        public tbl_MainMenu MainMenu { get; set; }
        public List<tbl_SubMenu> SubMenuList { get; set; }
    }


    public class ModuleSubModuleMenu
    {
        public tbl_MainMenu Module { get; set; }
        public IList<ModuleMenuModel> ModuleMenus { get; set; }
    }

    public class ModuleMenuModel
    {
        public tbl_SubMenu ModuleMenu { get; set; }
        public IList<tbl_SubMenu> ModuleSubMenus { get; set; }
    }

}
