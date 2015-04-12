var Exchange;

Exchange = (function() {
  var balanceHub, tickerHub, tradesHub;

  function Exchange(name, pair) {
    this.name = name;
    this.pair = pair;
    this.connectHubs();
  }

  tickerHub = $.connection.tickerHub;

  tradesHub = $.connection.tradesHub;

  balanceHub = $.connection.balanceHub;

  Exchange._trades = [];

  Exchange.prototype.connectHubs = function() {
    var _this = this;
    return $.connection.hub.start().done(function() {
      tickerHub.server.requestTicker(_this.name, _this.pair);
      tradesHub.server.requestTrades(_this.name, _this.pair);
      return balanceHub.server.requestBalance(_this.name, _this.pair);
    });
  };

  Exchange.prototype.updateTicker = function() {
    var _this = this;
    return tickerHub.client.update = function(exchange, price) {
      return $('#ticker-' + exchange).text(price);
    };
  };

  Exchange.prototype.updatePrice = function(x) {
    var _this = this;
    return tickerHub.client.update = function(exchange, price) {
      return x(exchange, price);
    };
  };

  Exchange.prototype.updateBalance = function(x) {
    var _this = this;
    return balanceHub.client.update = function(exchange, result) {
      return x(exchange, result);
    };
  };

  Exchange.prototype.updateTrades = function(x) {
    var _this = this;
    return tradesHub.client.update = function(exchange, items) {
      return x(exchange, items);
    };
  };

  return Exchange;

})();
