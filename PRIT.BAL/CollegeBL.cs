using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRIT.Entity;
using PRIT.DAL;

namespace PRIT.BAL
{
    public class CollegeBL 
    {
        CollegeDL collegeDL = new CollegeDL();
        PRITEntities db = new PRITEntities();

        public void AddEditCollege(tbl_Colleges obj)
        {
            try
           {                
                collegeDL.AddEditCollege(obj);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void DeleteCollege(int Id)
        {
            collegeDL.DeleteCollege(Id);
        }



        public List<Entity.ModuleSubModuleMenu> GetModuleMenu()
        {
            List<Entity.ModuleSubModuleMenu> objmodulesubMenu = new List<ModuleSubModuleMenu>();
            try
            {
                
                objmodulesubMenu = GetModuleSubModuleMenu();
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
            return objmodulesubMenu;
            //return PartialView("_ModuleMenuSubMenu");
        }
        

        /// <summary>
        /// 31-07-2017 - Ghanshyam - Get List of Modules and ModuleMenus
        /// </summary>
        /// <returns>List of ModelSubModelMenu</returns>
        public List<ModuleSubModuleMenu> GetModuleSubModuleMenu()
        {
            List<ModuleSubModuleMenu> ModelSubModelMenuList = new List<ModuleSubModuleMenu>();
            try
            {
                var Modules = db.tbl_MainMenu.OrderBy(x => x.Id).ToList();
                
                
                foreach (var module in Modules)
                {
                    //module.IconUrl = path + "\\" + module.IconUrl;
                    //module.IconUrl = commonMasterBL.GetImageFromByteArray(module.IconUrl);

                    var moduleMenuList = db.tbl_SubMenu.Where(MM => MM.MainMenuId == module.Id && MM.Level1SubMenuId == null && MM.Active).OrderBy(mm => mm.UiPosition).ToList();

                    List<ModuleMenuModel> moduleMenuModelList = new List<ModuleMenuModel>();

                    foreach (var moduleMenu in moduleMenuList)
                    {
                        var moduleSubMenuList = db.tbl_SubMenu.Where(MM => MM.MainMenuId == module.Id && MM.Level1SubMenuId != 0 && MM.Level1SubMenuId == moduleMenu.Id && MM.Active).OrderBy(mm => mm.UiSubPosition).ToList();
                        moduleMenuModelList.Add(new ModuleMenuModel
                        {
                            ModuleMenu = moduleMenu,
                            ModuleSubMenus = moduleSubMenuList
                        });
                    }

                    ModelSubModelMenuList.Add(new ModuleSubModuleMenu
                    {
                        Module = module,
                        ModuleMenus = moduleMenuModelList
                    });
                }

            }
            catch (Exception ex)
            {
                //ExceptionManager.Publish(ex, MethodBase.GetCurrentMethod().DeclaringType.Namespace + "-" + MethodBase.GetCurrentMethod().DeclaringType.Name, "FrameWork");
                throw ex;
            }
            return ModelSubModelMenuList;
        }





    }
}
