---
layout: default
title:  "Examples - Progress Notification"
categories: progress events
breadcrumbtext2: "Examples"
breadcrumblink2: "examples"
---

	You can get notified about the progress of the operations
	of the engines and DataLinks.
	This can be used in all cases. For example, when you write a file,
	the engine knows the total records to be written and the record
	currently being processed.  This information is used to provide an
	accurate notification. However, if you read a file and the engine only
	knows the current record and not the total record then the total count
	will be -1.

	The usage is simple, you must to instantiate the engine and before
	any operation, you must call the SetProgressHandler:

{% highlight csharp %}
  FileHelperEngine engine = new FileHelperEngine(typeof(Orders));

  engine.SetProgressHandler(new ProgressChangeHandler(ProgressChange));
    
  engine.ReadFile(... 
{% endhighlight %}

and in your Progress change method:

{% highlight csharp %}
    private void ProgressChange(ProgressEventArgs e)
    {
        prog1.PositionMax = e.ProgressTotal;
        prog1.Position = e.ProgressCurrent;
        prog1.Text = "Record " + e.ProgressCurrent.ToString();

        Application.DoEvents();
    }
{% endhighlight %} 


      Check the demo application for an example that looks like:
        <img src="progress.png" alt="progress bar"/>

      In the distribution you can find My XpProgressBar to use with
              the FileHelpers library.
