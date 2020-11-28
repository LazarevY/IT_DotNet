using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gtk;

namespace Task07GUI
{
    public class MethodInvokeWidget<T> : Grid
    {
        private Button _invokeButton;
        private MethodInfo MethodInfo;
        private List<IGetValueWidget> Widgets = new List<IGetValueWidget>();
        private Type Type;

        public MethodInvokeWidget(Type type, MethodInfo methodInfo)
        {
            Type = type;
            MethodInfo = methodInfo;
            ColumnHomogeneous = true;
            RowHomogeneous = true;
            InitForm();
        }

        private void InitForm()
        {
            ParameterInputCreater creater = new ParameterInputCreater();
            ParameterInfo[] parameterInfos = MethodInfo.GetParameters();
            this.InsertRow(0);
            this.InsertRow(0);

            for (var i = 0; i < parameterInfos.Length + 2; ++i)
                InsertColumn(0);
            _invokeButton = new Button("Invoke");
            _invokeButton.Clicked += Invoke;
            Attach(new Label(MethodInfo.Name), 0, 0, 1, 2);
            Attach(_invokeButton, 1, 0, 1, 2);
            for (int paramIndex = 0; paramIndex < parameterInfos.Length; ++paramIndex)
            {
                var name = parameterInfos[paramIndex].Name;
                var type = parameterInfos[paramIndex].ParameterType;
                var getValueWidget = creater.Create(type);
                Widgets.Add(getValueWidget);
                Attach((Widget) getValueWidget, paramIndex + 2, 1, 1, 1);
                Attach(new Label(name), paramIndex + 2, 0, 1, 1);
            }

            ShowAll();
        }

        private void Invoke(object? sender, EventArgs eventArgs)
        {
            var instance = Activator.CreateInstance(Type);
            var parameters = Widgets.Select(widget => widget.GetCurrentValue());
            MethodInfo.Invoke(instance, parameters.ToArray());
        }
    }
}