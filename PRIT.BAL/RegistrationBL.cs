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
                RegistrationDL registrationDL = new RegistrationDL();
                if (obj.Id > 0)
                {
                    obj.ModifiedDate = DateTime.Now;                   
                    obj.ModifiedBy = userName;
                   
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

                        obj.UserSalt = keyNew;
                    }

                    obj.Guid = guid;
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
            RegistrationDL registrationDL = new RegistrationDL();
            registrationDL.DeleteRegisteredUser(Id);
        }



        #region Login button click functionality..

        /// <summary>
        /// -Login Verification.  
        /// </summary>
        /// <param name="Email">Username</param>
        /// <param name="Password">Password</param>
        /// <returns>Return User values</returns>
        public tbl_Registration CheckUserNameExistsOrNot(string Email)
        {
            tbl_Registration user = new tbl_Registration();
            try
            {
                RegistrationDL userDB = new RegistrationDL();
                user = userDB.GetUserDetailByMail(Email);
            }
            catch (Exception ex)
            {
               // ExceptionManager.Publish(ex, MethodBase.GetCurrentMethod().DeclaringType.Namespace + "-" + MethodBase.GetCurrentMethod().DeclaringType.Name, "FrameWork");
                throw ex;
            }
            return user;
        }

        /// <summary>
        /// -Login Verification. 
        /// </summary>
        /// <param name="Email">Username</param>
        /// <param name="Password">Password</param>
        /// <returns>Return User values</returns>
        public tbl_Registration LoginVerification(string Email, string Password)
        {
            tbl_Registration user = null;
            try
            {
                //int saltkey=commonBL.GenrateSalt();
                //call method to get userdetail by email
                RegistrationDL userDB = new RegistrationDL();
                user = userDB.GetUserDetailByMail(Email);
                if (user != null)
                    if (!ComparePassword(Password, user.Password, user.UserSalt))
                    {
                        user = null;
                    }
            }
            catch (Exception ex)
            {
                //ExceptionManager.Publish(ex, MethodBase.GetCurrentMethod().DeclaringType.Namespace + "-" + MethodBase.GetCurrentMethod().DeclaringType.Name, "FrameWork");
                throw ex;
            }
            return user;
        }


        /// <summary>
        /// it will check whether user input password match with password stored in database for particular email id 
        /// </summary>
        /// <param name="Password"></param>
        /// <param name="EncryptedPassword"></param>
        /// <param name="Saltphrase"></param>
        /// <returns></returns>
        public static bool ComparePassword(string Password, string EncryptedPassword, string Saltphrase)
        {
            try
            {
                var DecryptedPassword = DecryptPassword(EncryptedPassword, Saltphrase);
                if (DecryptedPassword == Password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }



        /// <summary>
        /// it will decrypt password  
        /// </summary>
        /// <param name="Password">Encrypted password</param>
        /// <param name="Saltphrase">user salt</param>
        /// <returns>decrypted passord string</returns>
        public static string DecryptPassword(string Password, string Saltphrase)
        {
         

            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            //SHA512Managed HashProvider = new SHA512Managed();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            string Passphrase = Saltphrase;//"nEo@^!n@23";
            byte[] Results;


            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            try
            {

                byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

                // Step 2. Create a new TripleDESCryptoServiceProvider object


                // Step 3. Setup the decoder
                TDESAlgorithm.Key = TDESKey;
                TDESAlgorithm.Mode = CipherMode.ECB;
                TDESAlgorithm.Padding = PaddingMode.PKCS7;

                // Step 4. Convert the input string to a byte[]
                byte[] DataToDecrypt = Convert.FromBase64String(Password);

                // Step 5. Attempt to decrypt the string

                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            catch (Exception Ex)
            {
                return "Some exception occured" + Ex;
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }


        #endregion...End of Login button click functionality..




    }
}
