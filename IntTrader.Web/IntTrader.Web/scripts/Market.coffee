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
        @updateTicker()
        @updateTrades()
        
    tickerHub = $.connection.tickerHub
    tradesHub = $.connection.tradesHub
    
    @_trades = [];
    
    connectHubs: ->        
        $.connection.hub.start().done =>
            tickerHub.server.requestTicker @name, @pair
            tradesHub.server.requestTrades @name, @pair
    
    updateTicker: ->
        tickerHub.client.update = (exchange,price) =>
            $('#ticker-' + exchange).text(price)
            
    updateTrades: ->
        tradesHub.client.update = (exchange,items) =>            
            @appendTrade(exchange,i) for i in items
            undefined
    
    appendTrade: (exchange,i) =>
        if i.Side.Value == 'sell' then c  = 'sell' else c = 'buy'        
        $('#trades-' + exchange).append("<tr><td>#{i.TimestampString}</td><td class='#{c}'>#{i.Price}</td><td>#{i.Amount}</td></tr>")