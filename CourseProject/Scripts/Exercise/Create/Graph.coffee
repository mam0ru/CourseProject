$ ->
    points = []

    calculateExpression = (expr,f,t,s) ->
        variable = f
        while variable < t + s/2
            point = [variable, expr.evaluate({x: variable})]
            points.push(point) 
            variable = variable + s
    
    $(document).ready ()->
        $("#addGraph").on 'click', (e) ->
            e.preventDefault()
            formula = $("#GraphFormula").val()
            from = parseFloat($("#RangeFrom").val())
            to = parseFloat($("#RangeTo").val())
            step = parseFloat($("#Step").val())
            expression = Parser.parse(formula)
            calculateExpression(expression,from,to,step)
            $.jqplot('chart3', [points], {  
                  series:[{showMarker:false}],
                  axes:{
                    xaxis:{
                      label:'X'
                    },
                    yaxis:{
                      label:'Y'
                    }
                  }
                  })
            return null