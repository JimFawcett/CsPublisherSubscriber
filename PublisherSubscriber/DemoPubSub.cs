///////////////////////////////////////////////////////////////////////////
// DemoPubSub.cs - demonstrate passing data from publisher to subscriber //
//                                                                       //
// Jim Fawcett, CSE681 - Software Modeling and Analysis, Fall 2016       //
///////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PublisherSubscriber
{
  public class Publisher
  {
    public EventHandler eventDel = null;  
    
    // will invoke with eventDel.invoke(object sender, EventArgs eva)

    public class Eva : EventArgs
    {
      public string msg { get; set; }
    }

    private Eva eva_ = new Eva();

    public Publisher(string message)
    {
      eva_.msg = message;
    }

    public void doEvents()
    {
      for(int i=0; i<10; ++i)
      {
        Eva temp = new Eva();
        temp.msg = eva_.msg + " #" + (i+1).ToString();
        Thread.Sleep(200);
        if (eventDel != null)
          eventDel.Invoke(this, temp);
      }
    }
  }

  public class Subscriber
  {
    public Subscriber(Publisher pub)
    {
      pub.eventDel += new EventHandler(notified);
    }
    public void notified(object sender, EventArgs eva)
    {
      // need to cast to Eva type to access msg

      Console.Write("\n  subscriber notified with this message: \"{0}\"", (eva as Publisher.Eva).msg);
    }
  }
  /* 
   * In order to use title and putLine, defined below, without the Utilities
   * qualifier we would have to move the Utilities class into a separate 
   * package and provide a using Utilities; statement at the top of this 
   * namespace.
   * 
   * There is another workaround shown, below, at the top of the Program class.
   */
  static class Utilities
  {
    public delegate void Title(string msg, char underline = '-'); 
    public static Title title = (msg, underline) =>
    {
      Console.Write("\n  {0}", msg);
      Console.Write("\n {0}", new string(underline, msg.Length + 2));
    };
    public delegate void PutLine(int numLines = 1);
    public static PutLine putLine = (numLines) => 
    {
      for (int i = 0; i < numLines; ++i) Console.WriteLine();
    };
  }

  class Program
  {
    /* next two lines enable use of title and putLine without the Utilities qualifier */

    static Utilities.Title title = (msg, underline) => { Utilities.title(msg, underline); };
    static Utilities.PutLine putLine = (numLines) => { Utilities.putLine(numLines); };

    static void Main(string[] args)
    {
      title("Demonstrating Eventhandler Processing");
      putLine();
      Publisher pub = new Publisher("this is demo");
      Subscriber sub = new Subscriber(pub);
      pub.doEvents();
      putLine(2);
    }
  }
}
