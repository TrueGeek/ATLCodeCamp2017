using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BringMeSoup.mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public INavigation Navigation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        bool SetProperty<T>(ref T storage, T value, Expression<Func<T>> propertyExpression)
        {
            var propertyName = GetPropertyName(propertyExpression);
            return SetProperty<T>(ref storage, value, propertyName);
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.NotifyPropertyChanged(propertyName);
            return true;
        }

        private string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            if (propertyExpression.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("Should be a member access lambda expression", "propertyExpression");
            }

            var memberExpression = (MemberExpression)propertyExpression.Body;
            return memberExpression.Member.Name;
        }

    }
}
