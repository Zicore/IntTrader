(function() {

  $(function() {
    var ticker;
    ticker = $.connection.tickerHub;
    $.connection.hub.start().done(function() {
      return $('#button').click(function() {
        return ticker.server.RequestTicker('bitfinex', 'btcusd');
      });
    });
    return ticker.client.updateTicker = function(price) {
      return $('#price').text(price);
    };
  });

}).call(this);
