1.ʹ��dapper .net ��Ϊ micro orm ���ݿ�����Ʊ�����poco����һ�£��ұ�������Ϊid���������ԡ�
��ҳ���Լ���չ�ģ�ֻ��������sql server 2005���ϣ�SqlMapperPagedExtensions�ࣩ��
SqlMapperExtensions 68��hack ȥ�������poco����

 2. var sql = "select u.*, uv.* from [user] u,[uservalidation] uv where u.id=uv.userid and uv.id=@id";
var userValid = conn.Query<User, UserValidation, UserValidation>(sql, (u, uv) =>
                {
                    uv.User = u;
                    return uv;
                }//ʹ��mapsʱҪ�ر�ע��select u.*, uv.*˳��Ҫ��Query<User, UserValidation˳��һ��