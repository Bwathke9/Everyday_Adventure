<header>

<!-- -->
# Everyday Adventure

_A Unity project_

</header>

### Description

Everyday Adventure is a 2D side-scrolling platformer game developed in Unity 6 using C#. Players navigate through three levels filled with traps and obstacles,
collecting tokens to increase their scores and competing on a global leaderboard. Each level ends with a minigame that rewards bonus points and power-ups,
while correct answers to trivia questions can restore player health. The game is designed for Windows PCs and stores high scores in a MySQL database hosted on Google Cloud.

### System Requirements

A windows PC capable of running the Unity 6 engine.

### Installation

<ul>
    <li>Download  & Install Unity Hub</li>
    <li>Download Unity 6000.0.46f1</li>
  <li>Installing plasticSCM</li>
  <ol>
    <li> go to https://plasticscm.com/download and download the correct file for your operating system</li>
    <li> install plasticscm</li>
    <li> log into Unity account</li>
  </ol>
<li>Installing Git LFS</li>
    <ol>
        <li> go to https://git-lfs.com and download the correct installer for your PC</li>
        <li> install Git LFS</li>
    </ol>
  <li>Cloning the repo</li>
  <ol>
    <li> open gitBash</li>
    <li> navigate to desired repo location</li>
    <li> run git lfs install
    <li> run git clone git@github.com:Bwathke9/Everyday_Adventure.git</li>
  </ol>
  <li>Opening the project</li>
  <ol>
    <li> open Unity Hub</li>
    <li> click Add > Add project from disk</li>
    <li> navigate to folder containing the repo and select it</li>
    <li> open project using editor version 6000.0.46f1</li>
  </ol>
</ul>

### Testing

To verify server connectivity we will use tests run within the Unity Testing Environment. The included tests check that our
connection script within the Unity client correctly calls the API on our server with both a POST and GET method.
<ol>
  <li> open the project within Unity Editor following instructions under the Installation header</li>
  <li> navigate to Window > General > Test Runner</li>
  <li> click Run All in the bottom right corner of the Test Runner page</li>
  <li> confirm that tests pass</li>
</ol>

### Usage
The following steps describe how to build and run the project.
<ol>
  <li> open project within Unity Editor following instructions under the Installation header</li>
  <li> navigate to File > Build Profiles</li>
  <li> ensure Windows is selected and click Build</li>
  <li> navigate to desired project build location and select the folder</li>
  <li> run Everyday_Adventure.exe in the folder after build is finished</li>
</ol>

