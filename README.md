<header>

<!-- -->
# Everyday Adventure

_A Unity project_

</header>

### Game Play

### System Requirements

### Initial setup

  <ul>
  <li>Download Unity 6000.0.46f1</li>
  <li>GIT LFS setup</li>
  <ul>
    <li>1 go to https://git-lfs.com/ and download the correct version for your operating system.</li>
    <li>1 open gitBash in the directory you want your project.</li>
    <li>1 type 'git lfs install' to install on your account</li>
    <li>type 'git config --global core.longpaths true'</li>
    <li>***Initial LFS setup steps***</li>
    <ul>
        <li>track  the needed file types by entering<br> 
          'git lfs track "*.unity,*.mp4,*.asset,*.fbx,*.psp,*.dlyb,*.dylib,*.apiupaterconfigs,*.TextWriter,*.dil,*/ArtifactDB,*.Runtime" '</li>
        <li>type 'git add .getattributes'</li>
        <li>type 'git commit -m "Tracking files with LFS"</li>
        <li>type 'git lfs migrate import -include="*.unity,*.mp4,*.asset,*.fbx,*.psp,*.dlyb,*.dylib,*.apiupaterconfigs,*.TextWriter,*.dil,*/ArtifactDB,*.Runtime" --everything</li>
        <li>type 'git push origin main -force'</li>
        <li>if needed reinstall LFS 'git lfs install'</li>
        <li>then 'git lfs push --all origin'</li>
      </ul>
    <li>clone the repository to your local drive</li>
    <li>git lfs install</li>
    <li>git lfs pull</li>
    </ul>
  <li>open the Everyday_Adventure file in Unity.</li>
  <li>update the game in Unity</li>
  <li>save project</li>
  <li>close Unity</li>
  <li>'git add .'</li>
  <li>'git commit -m "message"'</li>
  <li>'git push'</li>
</ul>

