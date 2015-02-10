# CoffeeScript
$ ->
	ticker = $.connection.tickerHub
	$.connection.hub.start().done ->
		$('#button').click ->
		    ticker.server.requestTicker "bitfinex", "btcusd"
	
	ticker.client.updateTicker = (price) ->
		$('#price').text price