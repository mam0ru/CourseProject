$ ->
    $equations = []

    $images = []

    $tags = []

    $answers = []

    getFormulas = () ->
      elements = $("#formulas > #formula")
      $.each elements, (e,value) ->
        $equations.push(value.innerHTML)
      return $equations
 
    getImages = () ->
      im = $("#uploaded_images > img") 
      $.each im, (e,val) ->
        $images.push(val.src)
      $images  
    
    getTags = () ->
      t = $("#Tags > .row > [name='tag']")
      if t.length == 0
        return null
      $.each t, (e,val) ->
        $tags.push(val.value)
      $tags  
                
    getAnswers = () ->
      ans = $("#answer > .row > [name='answer']")
      $.each ans, (e,val) ->
        $answers.push(val.value)  
      $answers
    
    $(document).ready () ->       
      $('#sendExercise').on 'click', (e) ->
        answers = getAnswers()
        formulas = getFormulas()
        images = getImages()
        tags = getTags()
        category = $("select#Category").val()
        $('input#Category').val(category)    
        $('input#Answers').val(answers)
        $('input#Formulas').val(formulas)
        $('input#Pictures').val(images)
        $('input#Tags').val(tags)