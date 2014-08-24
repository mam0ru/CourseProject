$ ->
    points = []

    drawGraphs = (existing) ->
        $.each existing, (e,graph) ->
            graphDescription = graph.value
            graphDescription = JSON.parse(graphDescription)
            parent = graph.parentElement
            targetDiv = parent.children[1]
            targetDiv.id = "currentDiv"
            formula = graphDescription[0]
            from = parseFloat(graphDescription[1])
            to = parseFloat(graphDescription[2])
            step = parseFloat(graphDescription[3])
            expression = Parser.parse(formula)            
            calculateExpression(expression,from,to,step)
            draw()
            points = []
            targetDiv.id = ""
    
    calculateExpression = (expr,from,to,step) ->
        variable = from
        while variable < to + step/2
            point = [variable, expr.evaluate({x: variable})]
            points.push(point) 
            variable = variable + step    
    
    draw = () -> 
        $.jqplot('currentDiv', [points], {  
            series:[{showMarker:false}],
            axes:{
            xaxis:{
            },
            yaxis:{
            }
            }
            })      
                
    $(document).ready ()->
        existingGraphs = $("[name='GraphInfo']")
        drawGraphs(existingGraphs)
        $("#collapseFour")[0].className = 'panel-collapse collapse'        
        $("[name='target']")[0].setAttribute("data-toggle","collapse")