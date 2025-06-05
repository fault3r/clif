# samba

Samba allows you to share files and printers between Linux and Windows systems.
---

### ` sudo apt install samba ` to install samba.

#### 1. edit the samba configuration file
```
sudo nano /etc/samba/smb.conf
```

#### 2. add a new share definition at the end of the file
```
[SHARE-NAME]
    path = [PATH]
    available = yes
    valid users = [USERNAME]
    read only = no
    browsable = yes
    public = no
    writable = yes
```

#### 3. enter a password for samba access
```
sudo smbpasswd -a [USERNAME]
```

#### 4. restart samba service
```
sudo systemctl restart smbd
```

#### 5. update the firewall rules to allow samba traffic
```
sudo ufw allow samba
```

### accessing samba shares
#### ` smb://[IP-ADDRESS]/[SHARE-NAME] ` using a File Manager (Linux) 
#### ` \\[IP-ADDRESS]\[SHARE-NAME] ` using File Explorer (Windows)
