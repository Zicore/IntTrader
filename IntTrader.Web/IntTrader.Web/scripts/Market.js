var Exchange,
  __bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; };

Exchange = (function() {
  var tickerHub, tradesHub;

  function Exchange(name, pair) {
    this.name = name;
    this.pair = pair;
    this.appendTrade = __bind(this.appendTrade, this);

    this.connectHubs();
    this.updateTicker();
    this.updateTrades();
  }

  tickerHub = $.connection.tickerHub;

  tradesHub = $.connection.tradesHub;

  Exchange.prototype.connectHubs = function() {
    var _this = this;
    return $.connection.hub.start().done(function() {
      tickerHub.server.requestTicker(_this.name, _this.pair);
      return tradesHub.server.requestTrades(_this.name, _this.pair);
    });
  };

  Exchange.prototype.updateTicker = function() {
    var _this = this;
    return tickerHub.client.update = function(exchange, price) {
      return $('#ticker-' + exchange).text(price);
    };
  };

  Exchange.prototype.updateTrades = function() {
    var _this = this;
    return tradesHub.client.update = function(exchange, items) {
      var i, _i, _len;
      for (_i = 0, _len = items.length; _i < _len; _i++) {
        i = items[_i];
        _this.appendTrade(exchange, i);
      }
      return void 0;
    };
  };

  Exchange.prototype.appendTrade = function(exchange, i) {
    var c;
    if (i.Side.Value === 'sell') {
      c = 'sell';
    } else {
      c = 'buy';
    }
    return $('#trades-' + exchange).append("<tr><td>" + i.TimestampString + "</td><td class='" + c + "'>" + i.Price + "</td><td>" + i.Amount + "</td></tr>");
  };

  return Exchange;

})();
