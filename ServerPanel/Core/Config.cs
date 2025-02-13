class VM
{
    public string name { get; set; }
    public string os { get; set; }
}

class VMConfig
{
    public string panel_title { get; set; }
    public string password { get; set; }
    public List<VM> vms { get; set; }
}