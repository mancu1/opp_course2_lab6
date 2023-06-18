/*
Создать приложение, в котором генератор события, путем 
генерации одного события запрашивает у трех приемников 
некоторый ресурс. Каждый приемник сообщает, какое количество 
ресурса он может выделить. Для передачи информации 
использовать второй аргумент обработчика события.
*/


using System;

namespace lab6
{
    class ChangeEventArgs : EventArgs
    {
        int resourceSize;

        public int ResourceSize
        {
            get { return resourceSize; }
        }

        /*
        string name;

        public string Name
        {
            get { return name; }
        }
        */

        public ChangeEventArgs(int change)
        {
            /*string name*/
            this.resourceSize = change;
            /*this.name = name;*/
        }
    }

    class GenEvent // Генератор событий - издатель
    {
        public delegate void ChangeEventHandler
            (object source, ChangeEventArgs e);

        public event ChangeEventHandler OnChangeHandler;

        public void UpdateEvent(int resourceSize)
        {
            /*string name*/
            if (resourceSize == 0)
                return;
            /*name*/
            ChangeEventArgs e =
                new ChangeEventArgs(resourceSize);
            OnChangeHandler(this, e);
        }
    }

//Подписчик
    class RecEvent
    {
//Обработчик события
        void OnRecChange(object source, ChangeEventArgs e)
        {
            int resourceSize = e.ResourceSize;

            // get random int from 0 to 10

            int random = new Random().Next(0, 100);

            if (resourceSize > random)
            {
                Console.WriteLine("{0}: Не хватает ресурсов", this.GetHashCode());
            }
            else
            {
                Console.WriteLine("{0}: Могу выделить {1} ресурсов", this.GetHashCode(), random);
            }
        }

// в конструкторе класса осуществляется подписка
        public RecEvent(GenEvent gnEvent)
        {
            gnEvent.OnChangeHandler += new GenEvent.ChangeEventHandler(OnRecChange);
        }
    }

    class Class1
    {
        [STAThread]
        static void Main(string[] args)
        {
            GenEvent gnEvent = new GenEvent();
            RecEvent inventoryWatch1 = new RecEvent(gnEvent);
            RecEvent inventoryWatch2 = new RecEvent(gnEvent);
            RecEvent inventoryWatch3 = new RecEvent(gnEvent);
            gnEvent.UpdateEvent(2);
            gnEvent.UpdateEvent(25);
            gnEvent.UpdateEvent(50);
        }
    }
}