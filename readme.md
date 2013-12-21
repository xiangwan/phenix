1.使用dapper .net 作为 micro orm 数据库表名称必须与poco名称一致，且必须有名为id的主键属性。
分页是自己扩展的，只能运行在sql server 2005以上（SqlMapperPagedExtensions类）。
SqlMapperExtensions 68行hack 去除对象的poco属性

 2. var sql = "select u.*, uv.* from [user] u,[uservalidation] uv where u.id=uv.userid and uv.id=@id";
var userValid = conn.Query<User, UserValidation, UserValidation>(sql, (u, uv) =>
                {
                    uv.User = u;
                    return uv;
                }//使用maps时要特别注意select u.*, uv.*顺序要与Query<User, UserValidation顺序一致