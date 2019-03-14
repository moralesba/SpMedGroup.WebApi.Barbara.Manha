using Senai.SpMedGroup.WebApi.Manha.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SpMedGroup.WebApi.Manha.Interfaces
{
    public interface IUsuarioRepositorio
    {
        List<Usuario> Listar();
        Usuario BuscarEmailSenha(string email, string senha);
        Usuario BuscarUsuario(int usuarioId);
        void Cadastrar(Usuario usuario);
        void Alterar(Usuario usuario);
        void Deletar(int id);
    }
}
