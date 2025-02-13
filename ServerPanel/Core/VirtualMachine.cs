using System.Diagnostics;

namespace ServerPanel.Core;


public enum power_state
{
    on,
    off
}
public class VirtualMachine
{
    public string name { get; set; }
    public string os { get; set; }
    public power_state state { get; set; }
    public string VCpuCount { get; set; }
    public int MaxMemory { get; set; }
    public bool autostart { get; set; }
    

    public VirtualMachine(string name, string os, power_state state = power_state.off)
    {
        this.name = name;
        this.os = os;
        this.state = state;
        getInfos();
    }

    public void turnOn()
    {
        ExecuteBashCommand("virsh start " + this.name);
        
        
        if(this.state == power_state.on)
            return;
        
        power_state cur_state = power_state.off;
        
        while (cur_state != power_state.on)
        {
            string stateValue = ExecuteBashCommand($"LANG=C virsh dominfo {this.name} | grep 'State' | awk '{{print $2, $3}}'").Trim();
            cur_state = stateValue == "running" ? power_state.on : power_state.off;
        }
        this.state = cur_state;
    }

    public void turnOff()
    {
        ExecuteBashCommand("virsh shutdown " + this.name);
        
        //ExecuteBashCommand("virsh destroy " + this.name);
        
        if(this.state == power_state.off)
            return;
        
        power_state cur_state = power_state.on;
        
        while (cur_state != power_state.off)
        {
            string stateValue = ExecuteBashCommand($"LANG=C virsh dominfo {this.name} | grep 'State' | awk '{{print $2, $3}}'").Trim();
            cur_state = stateValue == "running" ? power_state.on : power_state.off;
        }
        this.state = cur_state;
    }

    private void getInfos()
    {
        string vmName = this.name;

        this.VCpuCount = ExecuteBashCommand($"LANG=C virsh dominfo {vmName} | grep 'CPU(s)' | awk '{{print $2}}'").Trim();

        this.MaxMemory = Convert.ToInt32(ExecuteBashCommand($"LANG=C virsh dominfo {vmName} | grep 'Max memory' | awk '{{print $3}}'").Trim()) / 1024;

        string autostartValue = ExecuteBashCommand($"LANG=C virsh dominfo {vmName} | grep 'Autostart' | awk '{{print $2}}'").Trim();
        this.autostart = autostartValue == "enable";

        string stateValue = ExecuteBashCommand($"LANG=C virsh dominfo {vmName} | grep 'State' | awk '{{print $2, $3}}'").Trim();
        this.state = stateValue == "running" ? power_state.on : power_state.off;
    }

    public void updateState()
    {
        string stateValue = ExecuteBashCommand($"LANG=C virsh dominfo {this.name} | grep 'State' | awk '{{print $2, $3}}'").Trim();
        this.state = stateValue == "running" ? power_state.on : power_state.off;
    }
    
    static string ExecuteBashCommand(string command)
    {
        Process process = new Process();
        process.StartInfo.FileName = "/bin/bash";
        process.StartInfo.Arguments = $"-c \"{command}\"";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (!string.IsNullOrEmpty(error))
        {
            return $"Error: {error}";
        }

        return output;
    }
}