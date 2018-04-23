using PRIT.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRIT.Entity;
using System.Security.Cryptography;

namespace PRIT.BAL
{

    public class RegistrationBL
    {
        RegistrationDL registrationDL = new RegistrationDL();
      //  PRITEntities db = new PRITEntities();

        /// <summary>
        /// 2017/05/18 - Rahul Patil - 
        /// Returns Register User Data
        /// </summary>
        /// <param name="user"></param>
        /// <returns>register user data</returns>
        public long Registration(tbl_Registration obj, string userName)
        {
            try
            {

                if (obj.Id > 0)
                {
                    obj.ModifiedDate = DateTime.Now;
                   
                    obj.ModifiedBy = userName;

                    //tbl_Registration lst = (from p in db.tbl_Registration
                    //           where p.Id == obj.Id
                    //           select p).FirstOrDefault();


                    //var lst = db.tbl_Registration.Where(Ind => Ind.Id == obj.Id).FirstOrDefault();
                    //var lst1 = db.tbl_Registration.FirstOrDefault();
                    //var lst2 = db.tbl_Registration.First();
                    //var lst3 = db.tbl_Registration.Where(Ind => Ind.Id == obj.Id).First();

                    ////modify individual information                                    
                    //lst.FullName = obj.FullName;
                    //lst.Email = obj.Email;
                    //lst.ContactNo = obj.ContactNo;
                    //lst.Designation = obj.Designation;
                    //lst.CollegeID = obj.CollegeID;
                    //lst.RoleId = obj.RoleId;
                    //lst.UserName = obj.UserName;
                    //lst.Password = obj.Password;



                    //var a = new tbl_Registration
                    //{
                    //    FullName = lst[0].FullName,
                    //    Email = lst[0].Email,
                    //    ContactNo = lst[0].ContactNo,
                    //    Designation = lst[0].Designation,
                    //    CollegeID = lst[0].CollegeID,
                    //    RoleId = lst[0].RoleId,
                    //    UserName = lst[0].UserName,
                    //    Password = lst[0].Password
                    //};

                    return registrationDL.Registration(obj);
                }
                else
                {

                    Guid guid = Guid.NewGuid();
                    if (!string.IsNullOrEmpty(obj.Password))
                    {
                        var keyNew = SaltPhrase();
                        var password = EncryptPassword(obj.Password, keyNew);
                        obj.Password = password;

                        ////obj.UserSalt = keyNew;
                    }

                    ////obj.Guid = guid;
                    if (obj.RoleId == null || obj.RoleId == 0)
                        obj.RoleId = 2;
                    obj.CreatedDate = DateTime.Now;
                    obj.IsActive = true;
                    obj.CreatedBy = userName;

                    return registrationDL.Registration(obj);


                }


            }
            catch (Exception ex)
            {
                return 0;
                // throw ex;
            }
        }


        #region AES Emcryption
        /// <summary>
        /// Encrypt Password.  
        /// </summary>
        /// <param name="plaintext">Password</param>
        /// <returns>Encrypted password</returns>
        public static string EncryptPassword(string Password, string Saltphrase)
        {
            try
            {
                string Passphrase = Saltphrase; //"nEo@^!n@23";
                string Message = Password;
                byte[] Results;
                System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

                // Step 1. We hash the passphrase using MD5
                // We use the MD5 hash generator as the result is a 128 bit byte array
                // which is a valid length for the TripleDES encoder we use below

                //SHA512Managed HashProvider = new SHA512Managed();
                MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
                byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

                // Step 2. Create a new TripleDESCryptoServiceProvider object
                TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

                // Step 3. Setup the encoder
                TDESAlgorithm.Key = TDESKey;
                TDESAlgorithm.Mode = CipherMode.ECB;
                TDESAlgorithm.Padding = PaddingMode.PKCS7;

                // Step 4. Convert the input string to a byte[]
                byte[] DataToEncrypt = UTF8.GetBytes(Message);

                // Step 5. Attempt to encrypt the string
                try
                {
                    ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                    Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
                }
                finally
                {
                    // Clear the TripleDes and Hashprovider services of any sensitive information
                    TDESAlgorithm.Clear();
                    HashProvider.Clear();
                }


                // Step 6. Return the encrypted string as a base64 encoded string
                return Convert.ToBase64String(Results);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private static Random random = new Random();

        /// <summary>
        ///  it will genrate salt key    
        /// </summary>
        /// <returns></returns>
        public static string SaltPhrase()
        {
            try
            {

                int length = 8;
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                //StringBuilder builder = new StringBuilder();
                //builder.Append(RandomString(1, false));
                //builder.Append(RandomNumber(10, 99));
                //builder.Append(RandomSpecialCharacters());
                //builder.Append(RandomString(2, true));
                // builder.ToString();
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion


        public void DeleteRegisteredUser(int Id)
        {
            registrationDL.DeleteRegisteredUser(Id);
        }





    }
}
