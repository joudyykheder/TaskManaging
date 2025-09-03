


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

enum PriorityLevel { Low, Medium, High }

class TaskItem
{
    public int TaskId;
    public PriorityLevel Priority;
    public DateTime TaskCreatedAt;

    public TaskItem(int id, PriorityLevel priority)
    {
        TaskId = id;
        Priority = priority;
       TaskCreatedAt = DateTime.Now;
    }

    public void Print()
    {
        Console.WriteLine($"ID: {TaskId}, Priority: {Priority}, Date: {TaskCreatedAt}");
    }
}

class TaskManager
{
    TaskItem[] tasks;
    int count;

    LinkedList<TaskItem> completedTasks = new LinkedList<TaskItem>();
    Queue<TaskItem> urgentTasks = new Queue<TaskItem>();

    public TaskManager()
    {
        tasks = new TaskItem[3];
        tasks[0] = new TaskItem(1, PriorityLevel.Medium);
        tasks[1] = new TaskItem(2, PriorityLevel.High);
        tasks[2] = new TaskItem(3, PriorityLevel.Low);
        count = 3;
    }

    public void ShowTasks()
    {
        Console.WriteLine("\nAll Tasks:");
        for (int i = 0; i < count; i++)
            tasks[i].Print();
    }

    public void AddTask(int id, PriorityLevel priority)
    {
        TaskItem[] newTasks = new TaskItem[count + 1];
        for (int i = 0; i < count; i++)
            newTasks[i] = tasks[i];

        newTasks[count] = new TaskItem(id, priority);
        tasks = newTasks;
        count++;
        Console.WriteLine("Task added Successfully !.");
    }

    public void RemoveTask(int id)
    {
        int index = -1;
        for (int i = 0; i < count; i++)
        {
            if (tasks[i].TaskId == id)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            Console.WriteLine("Task not found.");
            return;
        }

        TaskItem[] newTasks = new TaskItem[count - 1];
        int j = 0;
        for (int i = 0; i < count; i++)
        {
            if (i != index)
                newTasks[j++] = tasks[i];
        }

        tasks = newTasks;
        count--;
        Console.WriteLine("Task Removed successfully !.");
    }

    public void CompleteTask(int id)
    {
        int index = -1;
        for (int i = 0; i < count; i++)
        {
            if (tasks[i].TaskId == id)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            Console.WriteLine("Task not found.");
            return;
        }

        completedTasks.AddLast(tasks[index]);

        TaskItem[] newTasks = new TaskItem[count - 1];
        int j = 0;
        for (int i = 0; i < count; i++)
        {
            if (i != index)
                newTasks[j++] = tasks[i];
        }

        tasks = newTasks;
        count--;
        Console.WriteLine("Task Marked As Completed.");
    }

    public void ShowCompletedTasks()
    {
        Console.WriteLine("\nCompleted Tasks:");
        LinkedListNode<TaskItem> node = completedTasks.First;
        while (node != null)
        {
            node.Value.Print();
            node = node.Next;
        }
    }

    public void AddUrgentTask(int id)
    {
        urgentTasks.Enqueue(new TaskItem(id, PriorityLevel.High));
        Console.WriteLine("Urgent task added Successfully !.");
    }

    public void ShowUrgentTasks()
    {
        Console.WriteLine("\nUrgent Tasks:");
        TaskItem[] array = urgentTasks.ToArray();

        for (int i = 0; i < array.Length;i++)
        {
            array[i].Print();
        }
    }

    public void SortByPriority()
    {
        for (int i = 0; i < count - 1; i++)
        {
            for (int j = 0; j < count - i - 1; j++)
            {
                if (tasks[j].Priority > tasks[j + 1].Priority)
                {
                    TaskItem temp = tasks[j];
                    tasks[j] = tasks[j + 1];
                    tasks[j + 1] = temp;
                }
            }
        }
        Console.WriteLine("Tasks sorted by priority.");
    }

    public void SortByDate()
    {
        QuickSort(tasks, 0, count - 1);
        Console.WriteLine("Tasks sorted by date.");
    }

    private void QuickSort(TaskItem[] arr, int low, int high)
    {
        if (low < high)
        {
            int pi = SplitDatePivot(arr, low, high);
            QuickSort(arr, low, pi - 1);
            QuickSort(arr, pi + 1, high);
        }
    }

    private int SplitDatePivot(TaskItem[] arr, int low, int high)
    {
        DateTime pivot = arr[high].TaskCreatedAt;
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j].TaskCreatedAt <= pivot)
            {
                i++;
                TaskItem temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        TaskItem temp2 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp2;

        return i + 1;
    }
}
class Program
{
    static void Main()
    {
        TaskManager manager = new TaskManager();

        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1 - Show All Tasks");
            Console.WriteLine("2 - Add Task");
            Console.WriteLine("3 - Remove Task");
            Console.WriteLine("4 - Complete Task");
            Console.WriteLine("5 - Show Completed Tasks");
            Console.WriteLine("6 - Add Urgent Task");
            Console.WriteLine("7 - Show Urgent Tasks");
            Console.WriteLine("8 - Sort Tasks by Priority");
            Console.WriteLine("9 - Sort Tasks by Date");
            Console.WriteLine("0 - Exit");

            Console.Write("Please Enter Your choice ? ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    manager.ShowTasks();
                    break;

                case "2":
                    Console.Write("Enter Task ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Choose Priority: 0 - Low, 1 - Medium, 2 - High");
                    int p = int.Parse(Console.ReadLine());
                    manager.AddTask(id, (PriorityLevel)p);
                    break;

                case "3":
                    Console.Write("Enter Task ID to remove: ");
                    int removeId = int.Parse(Console.ReadLine());
                    manager.RemoveTask(removeId);
                    break;

                case "4":
                    Console.Write("Enter Task ID to complete: ");
                    int completeId = int.Parse(Console.ReadLine());
                    manager.CompleteTask(completeId);
                    break;

                case "5":
                    manager.ShowCompletedTasks();
                    break;

                case "6":
                    Console.Write("Enter Urgent Task ID: ");
                    int urgentId = int.Parse(Console.ReadLine());
                    manager.AddUrgentTask(urgentId);
                    break;

                case "7":
                    manager.ShowUrgentTasks();
                    break;

                case "8":
                    manager.SortByPriority();
                    break;

                case "9":
                    manager.SortByDate();
                    break;

                case "0":
                    Console.WriteLine("Exit");
                    return;

                default:
                    Console.WriteLine("Error !");
                    break;
            }
        }
    }
}