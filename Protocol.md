Linus Dehner, Dario Bruckner
## App architecture (layers and layer contents/functionality)
 Getting of data from the backend was done in a Data-Access layer and further transformed into Class Models in the Buiness layer to than finally be localist in the UI in further Model Classes. These Models than have properties bound to the specific values to be shown on the UI. 


## Use cases
## UX, library decisions (where applicable), lessons learned
We choose to use MaterialDesign for our frontend design framework since it offers material design options to the standart wpf tools. This makes it not only better to look at but it also helps with customization and thus helped a lot during the development process.

A lesson we both learned was that the project did need more time to develop and with our time managment it was very stressful until the end. Furthermore we learned how a wpf project is correctly structured and managed and how data is correctly transfered
## Implemented design pattern
For the UI we used a wpf-Application with the classic MVVM - Model View ViewModel pattern. 

A seconed design pattern that the frontend implemented is the classic command pattern. In which a command encapsulates an action to be performed at a different moment.

## Unit testing decisions
## Unique feature
Our Unique feature is a service, which provides the current temperature and a verbal weather description of the starting point end destination of a tour. We are using the free version of the [OpenWeatherMap API](https://openweathermap.org/api) to get the live data. This API also provides a service that returns you the weather of a route, which we originally planned to use, but this service is only available in the highly paied version.
## Tracked time
Around 70-80 Hours per person
## Link to GIT
[https://github.com/ldehner/Tour-Planner](https://github.com/ldehner/Tour-Planner)
