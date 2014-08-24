﻿$ ->
    points = []

    createParentDiv = () ->
        parent = document.createElement('div')
        parent.className = 'row'
        return parent

    createChildDiv = () ->
        childDiv = document.createElement('div')
        childDiv.className = 'col-md-4'
        return childDiv
        
    createButton = (name,text) ->   
        button = document.createElement('button')
        button.name = name
        button.className = "btn btn-danger"
        button.innerText = text           
        return button   

    createInfoElement = (graph)->
        info = document.createElement('input')
        info.type = 'hidden'
        info.name = 'GraphInfo'
        info.value = JSON.stringify(graph) 
        return info

    createContainerForGraph = (graph) ->
        parent = createParentDiv()
        GraphInfo = createInfoElement(graph)      
        childDivForGraph = createChildDiv()
        childDivForGraph.className = "col-md-5 thumbnail"
        childDivForGraph.id = "currentDiv"
        childDivForButton = createChildDiv()    
        childButton = createButton("delete","Удалить")
        childDivForButton.appendChild childButton
        parent.appendChild GraphInfo
        parent.appendChild childDivForGraph
        parent.appendChild childDivForButton
        return parent

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
            step = (to - from)/100 if step < (to - from)/100 
            expression = Parser.parse(formula)
            graph = [formula,from,to,step]
            container = createContainerForGraph(graph)
            $('#listOfGraphs')[0].appendChild(container)
            calculateExpression(expression,from,to,step)
            $.jqplot('currentDiv', [points], {  
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
            $('#currentDiv')[0].id = ""
            points = []
            return null