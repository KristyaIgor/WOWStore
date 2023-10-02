
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace WOWStore.Services.Models;

public partial class Category: ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
  
    public string name { get; set; }
   
    public string icon { get; set; }
}

