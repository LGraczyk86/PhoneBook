using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PhoneBook.Models;

namespace PhoneBook.SQLManager
{
    public class SourceManager
    {
        protected SqlConnection Connection { get; set; }
        public List<PersonModel> Persons = new List<PersonModel>();

        public void show()
        {
            Open();
           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = $"Select * From People";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;
            
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    PersonModel temp = new PersonModel();
                    if (reader.GetValue(6) is DateTime)
                    {
                        temp.ID = Convert.ToInt32(reader.GetValue(0));
                        temp.FirstName = reader.GetValue(1).ToString();
                        temp.LastName = reader.GetValue(2).ToString();
                        temp.Phone = Convert.ToInt32(reader.GetValue(3));
                        temp.Email = reader.GetValue(4).ToString();
                        temp.Created = Convert.ToDateTime(reader.GetValue(5));
                        temp.Updated = Convert.ToDateTime(reader.GetValue(6));
                    }
                    else
                    {
                        temp.ID = Convert.ToInt32(reader.GetValue(0));
                        temp.FirstName = reader.GetValue(1).ToString();
                        temp.LastName = reader.GetValue(2).ToString();
                        temp.Phone = Convert.ToInt32(reader.GetValue(3));
                        temp.Email = reader.GetValue(4).ToString();
                        temp.Created = Convert.ToDateTime(reader.GetValue(5));
                    }
                    
                    Persons.Add(temp);
                }
            }
            Close();
        }

        public PersonModel GetPersonByID(int id)
        {
            Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM People WHERE ID = @id";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;

            SqlParameter sqlParameterID = new SqlParameter()
            {
                ParameterName = "@id",
                Value = id,
                DbType = DbType.Int32
            };
            cmd.Parameters.Add(sqlParameterID);
            
            SqlDataReader reader = cmd.ExecuteReader();
            int a = reader.FieldCount;
            PersonModel temp = new PersonModel();

            while (reader.Read())
            {
                if (reader.GetValue(6) is DateTime)
                {
                    temp.ID = Convert.ToInt32(reader.GetValue(0));
                    temp.FirstName = reader.GetValue(1).ToString();
                    temp.LastName = reader.GetValue(2).ToString();
                    temp.Phone = Convert.ToInt32(reader.GetValue(3));
                    temp.Email = reader.GetValue(4).ToString();
                    temp.Created = Convert.ToDateTime(reader.GetValue(5));
                    temp.Updated = Convert.ToDateTime(reader.GetValue(6));
                }
                else
                {
                    temp.ID = Convert.ToInt32(reader.GetValue(0));
                    temp.FirstName = reader.GetValue(1).ToString();
                    temp.LastName = reader.GetValue(2).ToString();
                    temp.Phone = Convert.ToInt32(reader.GetValue(3));
                    temp.Email = reader.GetValue(4).ToString();
                    temp.Created = Convert.ToDateTime(reader.GetValue(5));
                }
            }
            
            Close();
            return temp;
        }

        public void add(PersonModel personModel)
        {
            Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO People (FirstName, LastName, Phone, Email, Created) " +
                              "VALUES (@FirstName, @LastName, @Phone, @Email, @Created)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;

            SqlParameter sqlParameterFirstName = new SqlParameter()
            {
                ParameterName = "@FirstName",
                Value = personModel.FirstName,
                DbType = DbType.String
            };

            SqlParameter sqlParameterLastName = new SqlParameter()
            {
                ParameterName = "@LastName",
                Value = personModel.LastName,
                DbType = DbType.String
            };

            SqlParameter sqlParameterPhone = new SqlParameter()
            {
                ParameterName = "@Phone",
                Value = personModel.Phone,
                DbType = DbType.Int32
            };

            SqlParameter sqlParameterEmail = new SqlParameter()
            {
                ParameterName = "@Email",
                Value = personModel.Email,
                DbType = DbType.String
            };

            SqlParameter sqlParameterCreated = new SqlParameter()
            {
                ParameterName = "@Created",
                Value = DateTime.Now,
                DbType = DbType.DateTime
            };

            cmd.Parameters.Add(sqlParameterFirstName);
            cmd.Parameters.Add(sqlParameterLastName);
            cmd.Parameters.Add(sqlParameterPhone);
            cmd.Parameters.Add(sqlParameterEmail);
            cmd.Parameters.Add(sqlParameterCreated);

            cmd.ExecuteNonQuery();

            Close();
        }

        public void search(string name)
        {
            Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * From People WHERE LastName Like @name";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;

            SqlParameter sqlParameterName = new SqlParameter()
            {
                ParameterName = "@name",
                Value = $"%{name}%",
                DbType = DbType.String
            };
            cmd.Parameters.Add(sqlParameterName);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                bool a = reader.HasRows;
                while (reader.Read())
                {
                    PersonModel temp = new PersonModel();
                    if (reader.GetValue(6) is DateTime)
                    {
                        temp.ID = Convert.ToInt32(reader.GetValue(0));
                        temp.FirstName = reader.GetValue(1).ToString();
                        temp.LastName = reader.GetValue(2).ToString();
                        temp.Phone = Convert.ToInt32(reader.GetValue(3));
                        temp.Email = reader.GetValue(4).ToString();
                        temp.Created = Convert.ToDateTime(reader.GetValue(5));
                        temp.Updated = Convert.ToDateTime(reader.GetValue(6));
                    }
                    else
                    {
                        temp.ID = Convert.ToInt32(reader.GetValue(0));
                        temp.FirstName = reader.GetValue(1).ToString();
                        temp.LastName = reader.GetValue(2).ToString();
                        temp.Phone = Convert.ToInt32(reader.GetValue(3));
                        temp.Email = reader.GetValue(4).ToString();
                        temp.Created = Convert.ToDateTime(reader.GetValue(5));
                    }
                    Persons.Add(temp);
                }
            }
            Close();
        }

        public void remove(PersonModel personModel)
        {
            Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM People " +
                              "WHERE ID = @ID";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;

            SqlParameter sqlParameterID = new SqlParameter()
            {
                ParameterName = "@ID",
                Value = personModel.ID,
                DbType = DbType.Int32
            };
            cmd.Parameters.Add(sqlParameterID);

            cmd.ExecuteNonQuery();
            Close();
        }

        public void update(PersonModel personModel)
        {
            Open();
           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE People " +
                              "SET FirstName = @FirstName, " +
                              "LastName = @LastName, " +
                              "Phone = @Phone, " +
                              "Email =  @Email, " +
                              "Updated = @Updated " +
                              "WHERE ID = @ID";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;
            
            SqlParameter sqlParameterID = new SqlParameter()
            {
                ParameterName = "@ID",
                Value = personModel.ID,
                DbType = DbType.Int32
            };

            SqlParameter sqlParameterFirstName = new SqlParameter()
            {
                ParameterName = "@FirstName",
                Value = personModel.FirstName,
                DbType = DbType.String
            };

            SqlParameter sqlParameterLastName = new SqlParameter()
            {
                ParameterName = "@LastName",
                Value = personModel.LastName,
                DbType = DbType.String
            };

            SqlParameter sqlParameterPhone = new SqlParameter()
            {
                ParameterName = "@Phone",
                Value = personModel.Phone,
                DbType = DbType.Int32
            };

            SqlParameter sqlParameterEmail = new SqlParameter()
            {
                ParameterName = "@Email",
                Value = personModel.Email,
                DbType = DbType.String
            };

            SqlParameter sqlParameterUpdated = new SqlParameter()
            {
                ParameterName = "@Updated",
                Value = DateTime.Now,
                DbType = DbType.DateTime
            };

            cmd.Parameters.Add(sqlParameterFirstName);
            cmd.Parameters.Add(sqlParameterLastName);
            cmd.Parameters.Add(sqlParameterPhone);
            cmd.Parameters.Add(sqlParameterEmail);
            cmd.Parameters.Add(sqlParameterUpdated);
            cmd.Parameters.Add(sqlParameterID);

            cmd.ExecuteNonQuery();

            Close();
        }

        protected void Open()
        {
            try
            {
                SqlConnection connection = new SqlConnection();

                connection.ConnectionString = "Integrated Security=SSPI;" +
                                              "Data Source=.\\SQLEXPRESS01;" +
                                              "Initial Catalog=PhoneBook;";
                Connection = connection;
                Connection.Open();
                var connectState = connection.State; //zmeinna pomocnicza
                var nameOfDB = connection.Database; //zmienna pomocnicza
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected void Close()
        {
            Connection.Close();
        }
    }
}