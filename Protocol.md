Linus Dehner, Dario Bruckner
## App architecture (layers and layer contents/functionality)
 Getting of data from the backend was done in a Data-Access layer and further transformed into Class Models in the Buiness layer to than finally be localist in the UI in further Model Classes. These Models than have properties bound to the specific values to be shown on the UI. 

## UX, library decisions (where applicable), lessons learned
We choose to use MaterialDesign for our frontend design framework since it offers material design options to the standart wpf tools. This makes it not only better to look at but it also helps with customization and thus helped a lot during the development process.

A lesson we both learned was that the project did need more time to develop and with our time managment it was very stressful until the end. Furthermore we learned how a wpf project is correctly structured and managed and how data is correctly transfered.
## Implemented design pattern
For the UI we used a wpf-Application with the classic MVVM - Model View ViewModel pattern. 

A seconed design pattern that the frontend implemented is the classic command pattern. In which a command encapsulates an action to be performed at a different moment.

As a third design pattern, we have applied the repository pattern in the backend to ensure compliance with the SOLID principles and to guarantee the layered architecture.

## Unit testing decisions

When it comes to unit testing, we decided to test all the converter methods in the backend, as this is an essential building block on which the whole framework is built. If these methods do not work, nothing works. Besides, we didn't really know what we could test otherwise.

## Unique feature
Our Unique feature is a service, which provides the current temperature and a verbal weather description of the starting point end destination of a tour. We are using the free version of the [OpenWeatherMap API](https://openweathermap.org/api) to get the live data. This API also provides a service that returns you the weather of a route, which we originally planned to use, but this service is only available in the highly paied version.
## Tracked time
We estimate the time required per person at approximately 75 hours per person. Our time management could have been a bit better, as we put off a lot of things and it got a bit stressful towards the end.
## Link to GIT
[https://github.com/ldehner/Tour-Planner](https://github.com/ldehner/Tour-Planner)

## Other Links

[Wireframes](https://github.com/ldehner/Tour-Planner/blob/main/WireFrame_TourPlanner.pdf)

[Sequence Diagram](https://github.com/ldehner/Tour-Planner/blob/main/SequenceDiagram.png)

[Class Diagram API](https://github.com/ldehner/Tour-Planner/blob/main/ClassDiagrammRest.png)

[Class Diagram Client](https://github.com/ldehner/Tour-Planner/blob/main/ClassDiagrammClient.png)

[Use Case Diagram](https://github.com/ldehner/Tour-Planner/blob/main/UseCase.png)
