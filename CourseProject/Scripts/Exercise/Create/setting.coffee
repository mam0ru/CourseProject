﻿$ ->
    createParentDiv = () ->
        parent = document.createElement('div')
        parent.className = 'row'
        return parent

    createChildDiv = () ->
        childDiv = document.createElement('div')
        childDiv.className = 'col-md-4'
        return childDiv
    
    createInput = (name) ->
        input = document.createElement('input')
        input.type = 'text'
        input.name = name
        return input

    createButton = (name,text) ->   
        button = document.createElement('button')
        button.name = name
        button.className = "btn btn-default"
        button.innerText = text           
        return button         

    $jqXHRData = null

    createImageElement = (src) ->
        parent = createParentDiv()
        childImage = document.createElement('img')
        childImage.src = src
        childDivForImage = createChildDiv()
        childDivForImage.className = "col-md-8 thumbnail"
        childDivForButton = createChildDiv()    
        childDivForImage.appendChild childImage
        childButton = createButton("delete","Delete")
        childDivForButton.appendChild childButton
        parent.appendChild childDivForImage
        parent.appendChild childDivForButton
        return parent

    createFormulaElement = (formula) ->
        parent = createParentDiv()
        childDivForImage = createChildDiv()
        childDivForImage.className = "col-md-4"
        childDivForButton = createChildDiv()    
        childDivForImage.innerHTML = formula
        childDivForImage.setAttribute('name','AddEquation')
        childButton = createButton("delete","Delete")
        childDivForButton.appendChild childButton
        parent.appendChild childDivForImage
        parent.appendChild childDivForButton        
        return parent   

    createVideoContainer = (src)->
        parent = createParentDiv()
        childDivForVideo = createChildDiv()
        childDivForVideo.className = "col-md-7"
        childDivForButton = createChildDiv()    
        childDivForVideo.innerHTML = src
        childButton = createButton("delete","Delete")
        childDivForButton.appendChild childButton
        parent.appendChild childDivForVideo
        parent.appendChild childDivForButton        
        return parent   
        

    initFileUpload = () ->
      $('#imageupload').fileupload({
        url: '/Exercise/UploadImage',
        dataType: 'json',
        add: (e, data) -> 
            $jqXHRData = data
            $('.progress')[0].hidden = false
            $jqXHRData.submit()
        done: (event, data) -> 
            jsItem = createImageElement(data.result.path)
            $('#listOfPictures').append(jsItem)
            $('.progress')[0].hidden = true
        fail: (event, data) ->
            alert "ERROR"
            if data.files[0].error
              alert data.files[0].error
        progressall: (e, data) ->
            progress = parseInt(data.loaded / data.total * 100, 10)
            $('.progress > .progress-bar')[0].style.width = progress + '%'
      })

    $equations = []

    $graphs = []

    $videos = []
    
    $images = []    

    getImages = () ->
      im = $("#listOfPictures > .row > .thumbnail > img") 
      $.each im, (e,val) ->
        $images.push(val.src)
      $images      
            
    getVideos = ()->
        elements = $('iframe')
        $.each elements, (e,value) ->
            $videos.push(value.src)
        return $videos

    getFormulas = () ->
      elements = $("[name='AddEquation']")
      $.each elements, (e,value) ->
        img = value.children[0]
        $equations.push(img.src)
      return $equations

    getGraphs = ()->
        elements = $("[name='GraphInfo']")
        $.each elements, (e,graph) ->
            $graphs.push(graph.value)
        return $graphs


    $(document).ready () ->
        $(document).on 'click', '#addFormula', (e)->
            e.preventDefault()
            img = $('#equationToImg')[0]
            list = $("#listOfFormulas")[0]
            if img.value
                child = createFormulaElement(img.value)
                $('#equationToImg')[0].value = ""
                $('#equation')[0].src = ""
                $('#equationInput')[0].value = ""
                list.appendChild(child)
            return null
        $(document).on 'click', "[name='delete']", (e) ->
            e.preventDefault()
            $(this).unbind()
            parent1 = $(this).parent()
            parent2 = parent1.parent()
            parent2.remove()
            return null
        initFileUpload()
        $("#addVideo").on 'click', (e) ->
            e.preventDefault()
            parent = createVideoContainer($("#video").val())
            $("#listOfVideos").append parent
            $("#video").val("")
        $("#Submit").on 'click', (e) ->
            answers = $("#inputAnswers").textext()[0].hiddenInput().val()
            tags = $("#inputTags").textext()[0].hiddenInput().val()
            equation = getFormulas()
            equation = JSON.stringify(equation)
            images = getImages()
            images = JSON.stringify(images)
            videos = getVideos()
            videos = JSON.stringify(videos)
            graphs = getGraphs()
            graphs = JSON.stringify(graphs)
            $('input#Graphs').val(graphs)            
            $('input#Category').val($("select#Category").val())    
            $('input#Answers').val(answers)
            $('input#Formulas').val(equation)
            $('input#Tags').val(tags)
            $('input#Pictures').val(images)
            $('input#Videos').val(videos)
        return null