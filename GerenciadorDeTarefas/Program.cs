using System;
using System.Collections.Generic;

namespace Layout
{
    public static class Formatacao
    {
        public static void Cor(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }

        public static void ImprimirCabecalho()
        {
            Console.Clear();
            Cor("GERENCIADOR DE TAREFAS", ConsoleColor.Cyan);
            Console.WriteLine("1 - Adicionar Tarefa");
            Console.WriteLine("2 - Listar Tarefas");
            Console.WriteLine("3 - Concluir Tarefa");
            Console.WriteLine("4 - Remover Tarefa");
            Console.WriteLine("0 - Sair");
        }
    }
}

namespace Tarefas
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Concluida { get; set; }

        public Tarefa(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
            Concluida = false;
        }

        public void ExibirTarefa()
        {
            string status = Concluida ? "[X]" : "[ ]";
            Console.WriteLine($"{status} ID: {Id} - {Descricao}");
        }
    }
}

namespace GerenciarTarefa
{
    public static class Gerenciador
    {
        private static List<Tarefas.Tarefa> listaTarefas = new List<Tarefas.Tarefa>();
        private static int contadorId = 1;

        public static void AdicionarTarefa(string descricao)
        {
            listaTarefas.Add(new Tarefas.Tarefa(contadorId++, descricao));
            Layout.Formatacao.Cor("Tarefa adicionada com sucesso!", ConsoleColor.Green);
        }

        public static void ListarTarefa()
        {
            Layout.Formatacao.Cor("A lista com todas as tarefas:", ConsoleColor.Yellow);
            foreach (var tarefa in listaTarefas)
            {
                tarefa.ExibirTarefa();
            }
        }

        public static void ConcluirTarefa(int id)
        {
            var tarefa = listaTarefas.Find(t => t.Id == id);
            if (tarefa != null)
            {
                tarefa.Concluida = true;
                Layout.Formatacao.Cor("Tarefa concluída!", ConsoleColor.Green);
            }
        }

        public static void RemoverTarefa(int id)
        {
            var tarefa = listaTarefas.Find(t => t.Id == id);
            if (tarefa != null)
            {
                listaTarefas.Remove(tarefa);
                Layout.Formatacao.Cor("Tarefa removida com sucesso!", ConsoleColor.Red);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        int opcao;
        do
        {
            Layout.Formatacao.ImprimirCabecalho();
            Console.Write("Escolha uma opção: "); 
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.Write("Digite a descrição da tarefa: ");
                    string descricao = Console.ReadLine();
                    GerenciarTarefa.Gerenciador.AdicionarTarefa(descricao);
                    break;
                case 2:
                    GerenciarTarefa.Gerenciador.ListarTarefa();
                    break;
                case 3:
                    Console.Write("Digite o ID da tarefa a concluir: ");
                    int idConcluir = int.Parse(Console.ReadLine());
                    GerenciarTarefa.Gerenciador.ConcluirTarefa(idConcluir);
                    break;
                case 4:
                    Console.Write("Digite o ID da tarefa a remover: ");
                    int idRemover = int.Parse(Console.ReadLine());
                    GerenciarTarefa.Gerenciador.RemoverTarefa(idRemover);
                    break;
                case 0:
                    Layout.Formatacao.Cor("Saindo...", ConsoleColor.Red);
                    break;
                default:
                    Layout.Formatacao.Cor("Opção inválida!", ConsoleColor.Red);
                    break;
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();

        } while (opcao != 0);
    }
}
