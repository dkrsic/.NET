$data = [Console]::In.ReadToEnd()
$data | Add-Content -Path "C:\Users\Dominik\Documents\GitHub\.NET\.github\hooks\agent_log.txt"