'use strict';

app.factory('backendHubProxy', ['$rootScope', 'backendServerUrl',
  function ($rootScope, backendServerUrl) {

      function backendFactory(serverUrl, hubName) {
          var connection = $.hubConnection(backendServerUrl);
          var proxy = connection.createHubProxy(hubName);

          connection.start().done(function () { });

          var hub = {
              on: function(eventName, callback) {
                  proxy.on(eventName, function(result) {
                      $rootScope.$apply(function() {
                          if (callback) {
                              callback(result);
                          }
                      });
                  });
              },
              invoke: function(methodName, p1, p2, callback) {
                  proxy.invoke(methodName, p1, p2)
                      .done(function(result) {
                          $rootScope.$apply(function() {
                              if (callback) {
                                  callback(result);
                              }
                          });
                      });
              }
          };
          return hub;
      };

      return backendFactory;
  }]);