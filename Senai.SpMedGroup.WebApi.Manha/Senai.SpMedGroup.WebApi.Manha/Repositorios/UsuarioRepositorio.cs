using Microsoft.EntityFrameworkCore;
using Senai.SpMedGroup.WebApi.Manha.Context;
using Senai.SpMedGroup.WebApi.Manha.Domains;
using Senai.SpMedGroup.WebApi.Manha.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Senai.SpMedGroup.WebApi.Manha.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private string StringConexao = "Data Source=.\\SqlExpress; initial catalog = SpMedGroup; user id=sa; pwd=132";

        public List<Usuario> Listar()
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                return ctx.Usuario.ToList();
            }
        }

        public void Cadastrar(Usuario usuario)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Usuario.Add(usuario);
                ctx.SaveChanges();
            }
        }

        public Usuario BuscarEmailSenha(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string QuerySelect = "select id_usuario, email, senha, tipo_usuario from usuario where email = @email and senha = @senha";

                using (SqlCommand cmd = new SqlCommand(QuerySelect, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        Usuario usuario = new Usuario();

                        while (sdr.Read())
                        {
                            usuario.IdUsuario = Convert.ToInt32(sdr["id_usuario"]);
                            usuario.Email = sdr["email"].ToString();
                            usuario.TipoUsuario = Convert.ToInt16(sdr["tipo_usuario"]);
                        }
                        return usuario;
                    }
                }
                return null;
            }
        }
        public void Deletar(int id)
            {
                using (SpMedGroupContext ctx = new SpMedGroupContext())
                {
                    ctx.Usuario.Remove(ctx.Usuario.Find(id));
                    ctx.SaveChanges();
                }
            }

            public void Alterar(Usuario usuario)
            {
                using (SpMedGroupContext ctx = new SpMedGroupContext())
                {
                    Usuario usuarioExiste = ctx.Usuario.Find(usuario.IdUsuario);

                    usuarioExiste.IdUsuario = usuario.IdUsuario;
                    ctx.Usuario.Update(usuario);
                    ctx.SaveChanges();
                }
            }

        public Usuario BuscarUsuario(int usuarioId)
        {
            Usuario usuarioBuscado = new Usuario();

            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                usuarioBuscado = ctx.Usuario.Find(usuarioId);
            }

            return usuarioBuscado;
        }
    }
}


