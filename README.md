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
  <ul>
    <li>1. go to https://plasticscm.com/download and download the correct file for your operating system</li>
    <li>2. install plasticscm</li>
    <li>3. log into Unity account</li>
  </ul>
  <li>Cloning the repo</li>
  <ul>
    <li>1. open gitBash</li>
    <li>2. navigate to desired repo location</li>
    <li>3. run git clone git@github.com:Bwathke9/Everyday_Adventure.git</li>
  </ul>
  <li>Opening the project</li>
  <ul>
    <li>1. open Unity Hub</li>
    <li>2. click Add > Add project from disk</li>
    <li>3. navigate to folder containing the repo and select it</li>
    <li>4. open project using editor version 6000.0.46f1</li>
  </ul>
</ul>

### Testing

To verify server connectivity we will use tests run within the Unity Testing Environment. The included tests check that our
connection script within the Unity client correctly calls the API on our server with both a POST and GET method.
<ul>
  <li>1. open the project within Unity Editor following instructions under the Installation header</li>
  <li>2. navigate to Window > General > Test Runner</li>
  <li>3. click Run All in the bottom right corner of the Test Runner page</li>
  <li>4. confirm that tests pass</li>
</ul>

### Usage
The following steps describe how to build and run the project.
<ul>
  <li>1. open project within Unity Editor following instructions under the Installation header</li>
  <li>2. navigate to File > Build Profiles</li>
  <li>3. ensure Windows is selected and click Build</li>
  <li>4. navigate to desired project build location and select the folder</li>
  <li>5. run Everyday_Adventure.exe in the folder after build is finished</li>
</ul>

