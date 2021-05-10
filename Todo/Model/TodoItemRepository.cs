using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Todo.Model
{


    public class TodoItemRepository
    {
        public IConnectionProvider _ConnectionProvider;

        private IConnectionProvider ConnectionProvider
        {
            get
            {
                return _ConnectionProvider;
            }
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException("Connection could not be null");
                }

                this._ConnectionProvider = value;
            }
        }

        public TodoItemRepository(IConnectionProvider conn)
        {
            ConnectionProvider = conn;
            
        }

        public async Task<TodoItem> Create( TodoItem todo )
        {

            using (var conn = ConnectionProvider.GetConnection())
            {

                int result = await conn.ExecuteAsync($@"insert into Todo (ID, Name, IsCompleted) values ({todo.Id}, '{todo.Name}', {todo.IsCompleted});");

                if(result <= 0)
                {
                    throw new CreateTodoItemFailed("create Todo failed.")
                    {
                        id = todo.Id,
                        Name = todo.Name,
                        IsCompleted = todo.IsCompleted
                    };
                }
            }

            return todo;
        }

        public async Task<List<TodoItem>> SelectAll()
        {
            List<TodoItem> list;
            using(var conn = ConnectionProvider.GetConnection())
            {
                list = (await conn.QueryAsync<TodoItem>("SELECT * FROM Todo;")).AsList<TodoItem>();
            }

            return list;
        }

        public async Task<TodoItem> Select(long id)
        {
            TodoItem result;
            using (var conn = ConnectionProvider.GetConnection())
            {
                result = (await conn.QueryFirstAsync<TodoItem>($@"SELECT * FROM Todo where id = {id}"));
            }

            return result;
        }

        public async Task<bool> Update(TodoItem item)
        {
            int result = 0;
            using (var conn = ConnectionProvider.GetConnection())
            {
                result = (await conn.ExecuteAsync($@"UPDATE Todo set Name='{item.Name}', IsCompleted = {item.IsCompleted} where Id={item.Id};"));
            }
            return result == 1;
        }


        public async Task<bool> Delete(long id)
        {
            int result = 0;
            using(var conn = ConnectionProvider.GetConnection())
            {
                result = (await conn.ExecuteAsync($@"DELETE FROM Todo where id={id}"));
            }

            return result == 1;
        }
    }
}
