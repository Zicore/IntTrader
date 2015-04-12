# CoffeeScript
#$ ->
#	ticker = $.connection.tickerHub
#	$.connection.hub.start().done ->
#		$('#button').click ->
#		    ticker.server.requestTicker "bitfinex", "btcusd"
#	
#	ticker.client.updateTicker = (price) ->
#		$('#price').text price

class Exchange
    constructor: (@name, @pair) ->
        @connectHubs()
        
    tickerHub = $.connection.tickerHub
    tradesHub = $.connection.tradesHub
    balanceHub = $.connection.balanceHub
    
    @_trades = [];
    
    connectHubs: ->        
        $.connection.hub.start().done =>
            tickerHub.server.requestTicker @name, @pair
            tradesHub.server.requestTrades @name, @pair
            balanceHub.server.requestBalance @name, @pair
    
    updateTicker: ->
        tickerHub.client.update = (exchange,price) =>
            $('#ticker-' + exchange).text(price)
            
    updatePrice: (x) ->
        tickerHub.client.update = (exchange,price) =>
            x(exchange,price)
    
    updateBalance: (x) ->
        balanceHub.client.update = (exchange,result) =>
            x(exchange,result)

    updateTrades: (x) ->
        tradesHub.client.update = (exchange,items) =>            
            x(exchange,items)