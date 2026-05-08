using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns1_2
{
    public interface ICommand
    {
        void Execute();
    }

    public abstract class ACommand : ICommand
    {
        public abstract void Execute();
    }

    
    public class CommandManager
    {
        private static CommandManager instance;
        private List<ACommand> commandHistory;
        private Stack<ACommand> undoneCommands;
        private bool locked;
        private CommandManager()
        {
            commandHistory = new List<ACommand>();
            undoneCommands = new Stack<ACommand>();
            locked = false;
        }

        public static CommandManager Instance()
        {
            if (instance == null)
            {
                instance = new CommandManager();
            }
            return instance;
        }

        public void RegisterCommand(ACommand command)
        {
            if (locked) return;
            commandHistory.Add(command);
            undoneCommands.Clear();
        }
        
        public void Undo()
        {

            if (commandHistory.Count == 0) return;
            locked = true;
            undoneCommands.Push(commandHistory[commandHistory.Count - 1]);
            commandHistory.RemoveAt(commandHistory.Count - 1);
            for (int i = 0; i< commandHistory.Count; i++) { 
                commandHistory[i].Execute();
            }
            locked = false;
        }

        public void Redo()
        {
            locked = true;
            if (undoneCommands.Count == 0) return;
            commandHistory.Add(undoneCommands.Peek());
            undoneCommands.Pop();
            
            for (int i = 0; i < commandHistory.Count; i++)
            {
                commandHistory[i].Execute();
            }
            locked = false;
        }
    }

    public class InitApplicationCommand : ACommand
    {
        private IMatrix matrix;
        

        public InitApplicationCommand(IMatrix m)
        {
            matrix = m;
        }

        public override void Execute()
        {
            MatrixInit.Init(matrix, 0, 10.0);  
        }
        
        
    }

    
    public class SetMatrixValueCommand : ACommand
    {
        private IMatrix matrix;
        private int row;
        private int col;
        private double newValue;
       
        
        public SetMatrixValueCommand(IMatrix matrix, int row, int col, double val)
        {
            this.matrix = matrix;
            this.row = row;
            this.col = col;
            newValue = val;
        }
        
        public override void Execute()
        {
            matrix.SetElem(row, col, newValue);
        }
        
    }

}
