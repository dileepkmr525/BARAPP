using System.Data;
using BARAPP.Models;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BARAPP.DataAccess
{
    public  class datalayer
    {
        static string ConnectionString = "Server=(local);Database=bardatabase;Trusted_Connection=True;TrustServerCertificate=True;";
        public static bool validateCredentials(Credentials creds)
        {
            try
            {
                const string query = "SELECT COUNT(1) FROM Users WHERE UserId = @UserId AND password = @Passwd";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = creds.UserID;
                    command.Parameters.Add("@Passwd", SqlDbType.NVarChar).Value = creds.Password;

                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<Shop> getShops()
        {
            try
            {
                var shops = new List<Shop>();
                const string query = "SELECT shopname,description FROM shops";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Shop shop = new Shop();
                            shop.ShopName = reader.GetString(0);
                            shop.Description = reader.GetString(1);
                            shops.Add(shop);
                        }
                    }
                }

                return shops;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<Category> getCategories()
        {
            try
            {
                var Categories = new List<Category>();
                const string query = "SELECT categoryname,description FROM categories";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category category = new Category();
                            category.categoryname = reader.GetString(0);
                            category.Description = reader.GetString(1);
                            Categories.Add(category);
                        }
                    }
                }

                return Categories;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        

        public static List<Brand> getBrands()
        {
            try
            {
                var Brands = new List<Brand>();
                const string query = "SELECT brandname,categoryname from  brands";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Brand brand = new Brand();
                            brand.BrandName = reader.GetString(0);
                            brand.CategoryName = reader.GetString(1);
                            Brands.Add(brand);
                        }
                    }
                }

                return Brands;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Credentials> getUsers()
        {
            try
            {
                var users = new List<Credentials>();
                const string query = "SELECT userid,password from users";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Credentials credentials = new Credentials();
                            credentials.UserID = reader.GetString(0);
                            credentials.Password = reader.GetString(1);
                            users.Add(credentials);
                        }
                    }
                }

                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Brand getBrandDetails(string BrandName)
        {
            try
            {
                Brand brand = new Brand();
                string query = "SELECT * from brands where brandname='"+BrandName+"'";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            brand.BrandName = reader.GetString(0);
                            brand.CategoryName = reader.GetString(1);
                            brand.ModifiedDate = reader.GetDateTime(3);
                            brand.OpenStockQ = reader.GetInt32(4);
                            brand.CostPerCaseQ = reader.GetDecimal(5);
                            brand.CostPerBottleQ = reader.GetDecimal(6);
                            brand.SalePriceQ = reader.GetDecimal(7);
                            brand.OpenStockN=reader.GetInt32(8);
                            brand.CostPerCaseN=reader.GetDecimal(9);
                            brand.CostPerBottleN = reader.GetDecimal(10);
                            brand.SalePriceN = reader.GetDecimal(11);
                            brand.OpenStockP=reader.GetInt32(12);
                            brand.CostPerCaseP=reader.GetDecimal(13);
                            brand.CostPerBottleP=reader.GetDecimal(14);
                            brand.SalePriceP=reader.GetDecimal(15);
                            brand.OpenStockD=reader.GetInt32(16);
                            brand.CostPerCaseD=reader.GetDecimal(17);
                            brand.CostPerBottleD=reader.GetDecimal(18);
                            brand.SalePriceD = reader.GetDecimal(19);
                            brand.OpenStockL=reader.GetInt32(20);
                            brand.CostPerCaseL=reader.GetDecimal(21);
                            brand.CostPerBottleL=reader.GetDecimal(22);
                            brand.SalePriceL = reader.GetDecimal(23);
                            break;
                        }
                    }
                }

                return brand ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static bool ShopExists(Shop shop)
        {
            try
            {
                const string query = "SELECT CAST(COUNT(1) AS BIT) FROM shops WHERE shopname = @shopname";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@shopname", SqlDbType.NVarChar, 50).Value = shop.ShopName;

                    connection.Open();
                    object result = command.ExecuteScalar();

                    return result != null && (bool)result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddShop(Shop shop)
        {
            const string query = @"INSERT INTO Shops (ShopName, description) 
                            VALUES (@ShopName, @ShopDescription)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@ShopName", SqlDbType.NVarChar, 50).Value = shop.ShopName;
                command.Parameters.Add("@ShopDescription", SqlDbType.NVarChar, 500).Value = shop.Description;

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public static bool CategoryExists(Category category)
        {
            try
            {
                const string query = "SELECT CAST(COUNT(1) AS BIT) FROM categories WHERE categoryname = @categoryname";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@categoryname", SqlDbType.NVarChar, 50).Value = category.categoryname;

                    connection.Open();
                    object result = command.ExecuteScalar();

                    return result != null && (bool)result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddCategory(Category category)
        {
            const string query = @"INSERT INTO categories (categoryname, description) 
                            VALUES (@categoryname, @description)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@categoryname", SqlDbType.NVarChar, 50).Value = category.categoryname;
                command.Parameters.Add("@description", SqlDbType.NVarChar, 50).Value = category.Description;

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public static bool BrandExists(string brandname)
        {
            try
            {
                const string query = "SELECT CAST(COUNT(1) AS BIT) FROM brands WHERE brandname = @brandname";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@brandname", SqlDbType.NVarChar, 50).Value = brandname;

                    connection.Open();
                    object result = command.ExecuteScalar();

                    return result != null && (bool)result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddBrand(Brand brand)
        {
            const string query = @"INSERT INTO brands (brandname, categoryname,modifieddate,openstockQ,costpercaseQ,costperbottleQ,salepriceQ,openstockN,costpercaseN,costperbottleN,salepriceN,openstockP,costpercaseP,costperbottleP,salepriceP,openstockD,costpercaseD,costperbottleD,salepriceD,openstockL,costpercaseL,costperbottleL,salepriceL) 
                            VALUES (@brandname,@categoryname,@modifieddate,@openstockQ,@costpercaseQ,@costperbottleQ,@salepriceQ,@openstockN,@costpercaseN,@costperbottleN,@salepriceN,@openstockP,@costpercaseP,@costperbottleP,@salepriceP,@openstockD,@costpercaseD,@costperbottleD,@salepriceD,@openstockL,@costpercaseL,@costperbottleL,@salepriceL)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@brandname", SqlDbType.NVarChar, 100).Value = brand.BrandName;
                command.Parameters.Add("@categoryname", SqlDbType.NVarChar, 50).Value = brand.CategoryName;
                command.Parameters.Add("@modifieddate", SqlDbType.DateTime, 50).Value = brand.ModifiedDate;
                command.Parameters.Add("@openstockQ", SqlDbType.Decimal, 50).Value = brand.OpenStockQ;
                command.Parameters.Add("@costpercaseQ", SqlDbType.Decimal, 50).Value = brand.CostPerCaseQ;
                command.Parameters.Add("@costperbottleQ", SqlDbType.Decimal, 50).Value = brand.CostPerBottleQ;
                command.Parameters.Add("@salepriceQ", SqlDbType.Decimal, 50).Value = brand.SalePriceQ;

                command.Parameters.Add("@openstockN", SqlDbType.Decimal, 50).Value = brand.OpenStockN;
                command.Parameters.Add("@costpercaseN", SqlDbType.Decimal, 50).Value = brand.CostPerCaseN;
                command.Parameters.Add("@costperbottleN", SqlDbType.Decimal, 50).Value = brand.CostPerBottleN;
                command.Parameters.Add("@salepriceN", SqlDbType.Decimal, 50).Value = brand.SalePriceN;

                command.Parameters.Add("@openstockP", SqlDbType.Decimal, 50).Value = brand.OpenStockP;
                command.Parameters.Add("@costpercaseP", SqlDbType.Decimal, 50).Value = brand.CostPerCaseP;
                command.Parameters.Add("@costperbottleP", SqlDbType.Decimal, 50).Value = brand.CostPerBottleP;
                command.Parameters.Add("@salepriceP", SqlDbType.Decimal, 50).Value = brand.SalePriceP;

                command.Parameters.Add("@openstockD", SqlDbType.Decimal, 50).Value = brand.OpenStockD;
                command.Parameters.Add("@costpercaseD", SqlDbType.Decimal, 50).Value = brand.CostPerCaseD;
                command.Parameters.Add("@costperbottleD", SqlDbType.Decimal, 50).Value = brand.CostPerBottleD;
                command.Parameters.Add("@salepriceD", SqlDbType.Decimal, 50).Value = brand.SalePriceD;

                command.Parameters.Add("@openstockL", SqlDbType.Decimal, 50).Value = brand.OpenStockL;
                command.Parameters.Add("@costpercaseL", SqlDbType.Decimal, 50).Value = brand.CostPerCaseL;
                command.Parameters.Add("@costperbottleL", SqlDbType.Decimal, 50).Value = brand.CostPerBottleL;
                command.Parameters.Add("@salepriceL", SqlDbType.Decimal, 50).Value = brand.SalePriceL;



                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }


        public static bool updateBrand(Brand brand)
        {
            const string query = @"
        UPDATE Brands
        SET 
            ModifiedDate = @ModifiedDate,

            OpenStockQ = @OpenStockQ,
            CostPerCaseQ = @CostPerCaseQ,
            CostPerBottleQ = @CostPerBottleQ,
            SalePriceQ = @SalePriceQ,

            OpenStockN = @OpenStockN,
            CostPerCaseN = @CostPerCaseN,
            CostPerBottleN = @CostPerBottleN,
            SalePriceN = @SalePriceN,

            OpenStockP = @OpenStockP,
            CostPerCaseP = @CostPerCaseP,
            CostPerBottleP = @CostPerBottleP,
            SalePriceP = @SalePriceP,

            OpenStockD = @OpenStockD,
            CostPerCaseD = @CostPerCaseD,
            CostPerBottleD = @CostPerBottleD,
            SalePriceD = @SalePriceD,

            OpenStockL = @OpenStockL,
            CostPerCaseL = @CostPerCaseL,
            CostPerBottleL = @CostPerBottleL,
            SalePriceL = @SalePriceL
        WHERE brandname = @BrandName";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {               
                command.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = brand.ModifiedDate;
                command.Parameters.Add("@OpenStockQ", SqlDbType.Int).Value = brand.OpenStockQ;
                command.Parameters.Add("@CostPerCaseQ", SqlDbType.Decimal).Value = brand.CostPerCaseQ;
                command.Parameters.Add("@CostPerBottleQ", SqlDbType.Decimal).Value = brand.CostPerBottleQ;
                command.Parameters.Add("@SalePriceQ", SqlDbType.Decimal).Value = brand.SalePriceQ;

                command.Parameters.Add("@OpenStockN", SqlDbType.Int).Value = brand.OpenStockN;
                command.Parameters.Add("@CostPerCaseN", SqlDbType.Decimal).Value = brand.CostPerCaseN;
                command.Parameters.Add("@CostPerBottleN", SqlDbType.Decimal).Value = brand.CostPerBottleN;
                command.Parameters.Add("@SalePriceN", SqlDbType.Decimal).Value = brand.SalePriceN;

                command.Parameters.Add("@OpenStockP", SqlDbType.Int).Value = brand.OpenStockP;
                command.Parameters.Add("@CostPerCaseP", SqlDbType.Decimal).Value = brand.CostPerCaseP;
                command.Parameters.Add("@CostPerBottleP", SqlDbType.Decimal).Value = brand.CostPerBottleP;
                command.Parameters.Add("@SalePriceP", SqlDbType.Decimal).Value = brand.SalePriceP;

                command.Parameters.Add("@OpenStockD", SqlDbType.Int).Value = brand.OpenStockD;
                command.Parameters.Add("@CostPerCaseD", SqlDbType.Decimal).Value = brand.CostPerCaseD;
                command.Parameters.Add("@CostPerBottleD", SqlDbType.Decimal).Value = brand.CostPerBottleD;
                command.Parameters.Add("@SalePriceD", SqlDbType.Decimal).Value = brand.SalePriceD;

                command.Parameters.Add("@OpenStockL", SqlDbType.Int).Value = brand.OpenStockL;
                command.Parameters.Add("@CostPerCaseL", SqlDbType.Decimal).Value = brand.CostPerCaseL;
                command.Parameters.Add("@CostPerBottleL", SqlDbType.Decimal).Value = brand.CostPerBottleL;
                command.Parameters.Add("@SalePriceL", SqlDbType.Decimal).Value = brand.SalePriceL;

                command.Parameters.Add("@BrandName", SqlDbType.NVarChar).Value = brand.BrandName;

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }


        public static bool deleteBrand(string BrandName)
        {
            const string query = "DELETE FROM Brands WHERE BrandName = @BrandName";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@BrandName", SqlDbType.NVarChar, 150).Value = BrandName;

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public static bool UserExists(string userID)
        {
            try
            {
                const string query = "SELECT CAST(COUNT(1) AS BIT) FROM users WHERE userid = @userid";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@userid", SqlDbType.NVarChar, 50).Value = userID;

                    connection.Open();
                    object result = command.ExecuteScalar();

                    return result != null && (bool)result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddUser(Credentials creds)
        {
            const string query = @"INSERT INTO users (userid, password) 
                            VALUES (@userid, @pwd)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@userid", SqlDbType.NVarChar, 50).Value = creds.UserID;
                command.Parameters.Add("@pwd", SqlDbType.NVarChar, 50).Value = creds.Password;

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }


        public static Credentials getUser(string userid)
        {
            try
            {
                Credentials user = new Credentials();
                 string query = "SELECT * from users where userid='" + userid+"'";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.UserID = reader.GetString(0);
                            user.Password = reader.GetString(1);
                            break;
                        }
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static bool saveUser(Credentials creds)
        {
            const string query = "UPDATE Users SET password = @Passwd WHERE UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@Passwd", SqlDbType.NVarChar, 10).Value = creds.Password;
                command.Parameters.Add("@UserId", SqlDbType.NVarChar, 10).Value = creds.UserID;

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public static bool deleteUser(string UserID)
        {
            const string query = "DELETE FROM users WHERE userid = @UserID";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@UserID", SqlDbType.NVarChar, 10).Value = UserID;

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
    }
}
